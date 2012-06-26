using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class DoubleBufferedLayoutTable : TableLayoutPanel
    {
        public DoubleBufferedLayoutTable()
        {
            this.DoubleBuffered = true;
        }
    }
}
