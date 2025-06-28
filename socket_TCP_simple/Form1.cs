using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace socket_TCP_simple
{
    public partial class Form1 : Form
    {
     Controller contr;
        SynchronizationContext context;
        public Form1()
        {
            InitializeComponent();
            // Получим контекст синхронизации для текущего потока 
            contr = new Controller();
            context = new SynchronizationContext();

            contr.ServerRecieveEvent += ServerRecieve;//////////////////подписка на собітіе!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        private void ServerRecieve(string message)////////////метод кот реагирует на собітіе
        {
            
           context.Send( (m)=>listBox1.Items.Add(m),message);
        }



        // обслуживание очередного запроса будем выполнять в отдельном потоке

        
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(contr.ThreadForAccept));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
