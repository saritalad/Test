using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//Added by NuGet
using KafkaNet;
using KafkaNet.Model;
namespace KafkaDemo
{
    public partial class kafakamessaging : Form
    {
        Uri uri = new Uri("http://localhost:9092");
        string topic = "dotnettraining";

        public kafakamessaging()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Message", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string payload = txtMessage.Text.Trim();
            var sendMessage = new Thread(() =>
            {
                KafkaNet.Protocol.Message msg = new KafkaNet.Protocol.Message(payload);
                var options = new KafkaOptions(uri);
                var router = new BrokerRouter(options);
                var client = new Producer(router);
                client.SendMessageAsync(topic, new List<KafkaNet.Protocol.Message> { msg }).Wait();
            });
            sendMessage.Start();
            this.Clear();
        }

        void Clear()
        {
            this.txtMessage.Text = string.Empty;
            this.txtMessage.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Clear();
        }
    }
}
