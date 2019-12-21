using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace FileExplorer
{
    public class ListViewEx : ListView
    {
        private const int WmHscroll = 0x0114;
        private const int WmVscroll = 0x0115;
        private const int WmKeydown = 0x0100;
        private const int WmMousewheel = 0x020A;

        public event EventHandler ScrollEvent;

        [System.Security.Permissions.SecurityPermission
            (System.Security.Permissions.SecurityAction.LinkDemand, Flags =
             System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmVscroll ||
                m.Msg == WmHscroll ||
                m.Msg == WmKeydown ||
                m.Msg == WmMousewheel)
                if (ScrollEvent != null)
                    ScrollEvent(this, null);
            base.WndProc(ref m);
        }

        public void SetDoubleBuffered(bool value)
        {
            DoubleBuffered = value;
        }

        /// <summary>
        /// Метод для заполнения окна таблицы по ширине, растягивая столбец index
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        public void ResizeColumns(int index)
        {
            // получаем ширину списка
            var width = this.Size.Width;
            // сумма ширин столбцов, кроме столбца с индексом
            var sum = (from ColumnHeader column in this.Columns where column.Index != index select column.Width).Sum();
            // расчитаем высоту строки, используя настройки шрифта
            var g = Graphics.FromHwnd(this.Handle);
            var strHeight = g.MeasureString("Xxy", this.Font).Height;
            // расчитаем видимое количество строк
            var visiblerowcount = Convert.ToInt32(Math.Truncate(this.Height / strHeight)) - 1;
            // если вычисленное количество строк больше реального количества
            if (visiblerowcount > this.Items.Count)
                // то не учитываем ширину скроллбара
                this.Columns[index].Width = width - sum - 5;
            else
                // иначе - учитываем
                this.Columns[index].Width = width - sum - SystemInformation.VerticalScrollBarWidth - 5;
        }

    }

    // Implements the manual sorting of items by columns.
    public class ListViewItemComparer : IComparer
    {
        private readonly int _col;

        public ListViewItemComparer(int column)
        {
            _col = column;
        }

        public int Compare(object x, object y)
        {
            var itemX = (ListViewItem)x;
            var itemY = (ListViewItem)y;
            if (_col < itemX.SubItems.Count && _col < itemY.SubItems.Count)
                return String.CompareOrdinal(itemX.SubItems[_col].Text, itemY.SubItems[_col].Text);
            return 0;
        }
    }

    // Implements the manual reverse sorting of items by columns.
    public class ListViewItemReverseComparer : IComparer
    {
        private readonly int _col;
        public ListViewItemReverseComparer(int column)
        {
            _col = column;
        }
        public int Compare(object x, object y)
        {
            var itemX = (ListViewItem)x;
            var itemY = (ListViewItem)y;
            if (_col < itemX.SubItems.Count && _col < itemY.SubItems.Count)
                return String.CompareOrdinal(itemY.SubItems[_col].Text, itemX.SubItems[_col].Text);
            return 0;
        }
    }

    // Implements the manual sorting of items last column by date.
    public class ListViewItemDateComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var itemX = (ListViewItem)x;
            var itemY = (ListViewItem)y;
            var colX = itemX.SubItems.Count - 1;
            var colY = itemY.SubItems.Count - 1;
            var dt = DateTime.MinValue;
            if (itemX.SubItems[colX].Tag != null &&
                itemX.SubItems[colX].Tag.GetType() == dt.GetType() &&
                itemY.SubItems[colY].Tag != null &&
                itemY.SubItems[colY].Tag.GetType() == dt.GetType())
            {
                return DateTime.Compare((DateTime)itemX.SubItems[colX].Tag,
                    (DateTime)itemY.SubItems[colY].Tag);
            }
            return 0;
        }
    }

    // Implements the manual sorting of items last column by date.
    public class ListViewItemReverseDateComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var itemX = (ListViewItem)x;
            var itemY = (ListViewItem)y;
            var colX = itemX.SubItems.Count - 1;
            var colY = itemY.SubItems.Count - 1;
            var dt = DateTime.MinValue;
            if (itemX.SubItems[colX].Tag != null &&
                itemX.SubItems[colX].Tag.GetType() == dt.GetType() &&
                itemY.SubItems[colY].Tag != null &&
                itemY.SubItems[colY].Tag.GetType() == dt.GetType())
            {
                return DateTime.Compare((DateTime)itemY.SubItems[colY].Tag,
                    (DateTime)itemX.SubItems[colX].Tag);
            }
            return 0;
        }
    }
}
