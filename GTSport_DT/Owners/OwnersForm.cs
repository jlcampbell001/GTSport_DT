using GTSport_DT.General.KeySequence;
using Npgsql;
using System;
using System.Windows.Forms;

namespace GTSport_DT.Owners
{
    public partial class OwnersForm : Form
    {
        public OwnersForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            var cs = "Host=localhost;Username=postgres;Username=GTSport;Password=GTSport;Database=GTSport_Test";

            var con = new NpgsqlConnection(cs);
            con.Open();

            KeySequenceRepository keySequenceRepository = new KeySequenceRepository(con);

            textBox1.AppendText("-------------ADD--------------");
            textBox1.AppendText(Environment.NewLine);

            KeySequence keySequence1 = new KeySequence();
            keySequence1.TableName = "TEST";
            keySequence1.LastKeyValue = 10;

            keySequenceRepository.Save(keySequence1);

            var keySequences = keySequenceRepository.GetList();

            keySequences.ForEach(delegate (KeySequence keySequence)
            {
                string line = keySequence.TableName + "   " + keySequence.LastKeyValue;

                textBox1.AppendText(line);
                textBox1.AppendText(Environment.NewLine);
            });

            textBox1.AppendText("-------------CHANGE--------------");
            textBox1.AppendText(Environment.NewLine);

            keySequence1.LastKeyValue++;

            keySequenceRepository.Save(keySequence1);

            keySequences = keySequenceRepository.GetList();

            keySequences.ForEach(delegate (KeySequence keySequence)
            {
                string line = keySequence.TableName + "   " + keySequence.LastKeyValue;

                textBox1.AppendText(line);
                textBox1.AppendText(Environment.NewLine);
            });

            textBox1.AppendText("-------------DELETE--------------");
            textBox1.AppendText(Environment.NewLine);

            keySequenceRepository.Delete(keySequence1.TableName);

            keySequences = keySequenceRepository.GetList();

            keySequences.ForEach(delegate (KeySequence keySequence)
            {
                string line = keySequence.TableName + "   " + keySequence.LastKeyValue;

                textBox1.AppendText(line);
                textBox1.AppendText(Environment.NewLine);
            });

            con.Close();
        }
    }
}
