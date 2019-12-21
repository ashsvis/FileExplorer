using System.Windows.Forms;

namespace FileExplorer
{
    public partial class PropertiesForm : Form
    {
        public PropertiesForm(System.Collections.Specialized.NameValueCollection properties)
        {
            InitializeComponent();
            foreach (var key in properties.AllKeys)
            {
                var lvi = new ListViewItem(key);
                lvi.SubItems.Add(properties[key]);
                listView1.Items.Add(lvi);
            }
        }
    }
}
