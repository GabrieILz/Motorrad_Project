using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SQLite;

namespace Final_System
{
    public partial class Funcionarios : Form
    {
        public Funcionarios()
        {
            InitializeComponent();
            ArredondaCantosdoForm();
        }
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DA;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        private void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=MotorradDB.db;Version=3;New=False;Compress=true;");
        }

        private void LoadData()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select * from MotorradTB_FuncionariosCadastro";
            DA = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DA.Fill(DS);
            DT = DS.Tables[0];
            DataGrid_Funcionarios.DataSource = DT;
            sql_con.Close();
        }
        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
        public void ArredondaCantosdoForm()
        {

            GraphicsPath PastaGrafica = new GraphicsPath();
            PastaGrafica.AddRectangle(new System.Drawing.Rectangle(1, 1, this.Size.Width, this.Size.Height));


            PastaGrafica.AddRectangle(new System.Drawing.Rectangle(1, 1, 10, 10));
            PastaGrafica.AddPie(1, 1, 20, 20, 180, 90);


            PastaGrafica.AddRectangle(new System.Drawing.Rectangle(this.Width - 12, 1, 12, 13));
            PastaGrafica.AddPie(this.Width - 24, 1, 24, 26, 270, 90);


            PastaGrafica.AddRectangle(new System.Drawing.Rectangle(1, this.Height - 10, 10, 10));
            PastaGrafica.AddPie(1, this.Height - 20, 20, 20, 90, 90);


            PastaGrafica.AddRectangle(new System.Drawing.Rectangle(this.Width - 12, this.Height - 13, 13, 13));
            PastaGrafica.AddPie(this.Width - 24, this.Height - 26, 24, 26, 0, 90);

            PastaGrafica.SetMarkers();
            this.Region = new Region(PastaGrafica);
        }
        Point ArrastarCursor;
        Point ArrastarForm;
        bool Arrastando;

        private void Funcionarios_MouseUp(object sender, MouseEventArgs e)
        {
            Arrastando = false;
        }

        private void Funcionarios_MouseDown(object sender, MouseEventArgs e)
        {
            Arrastando = true;
            ArrastarCursor = Cursor.Position;
            ArrastarForm = this.Location;
        }

        private void Funcionarios_MouseMove(object sender, MouseEventArgs e)
        {
            if (Arrastando == true)
            {
                Point diferenca = Point.Subtract(Cursor.Position, new Size(ArrastarCursor));
                this.Location = Point.Add(ArrastarForm, new Size(diferenca));
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Arrastando = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Arrastando = true;
            ArrastarCursor = Cursor.Position;
            ArrastarForm = this.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Arrastando == true)
            {
                Point diferenca = Point.Subtract(Cursor.Position, new Size(ArrastarCursor));
                this.Location = Point.Add(ArrastarForm, new Size(diferenca));
            }
        }

        private void Btn_Adicionar_Click(object sender, EventArgs e)
        {
            string txtQuery = "insert into MotorradTB_FuncionariosCadastro(ID, Nome, CPF, RG, DataNascimento, DataEntrada, Endereco, Numero, Celular, Fixo, Email)Values('" + Txt_ID.Text + "','" + Txt_Nome.Text + "','" + Txt_CPF.Text + "','" + Txt_RG.Text + "','" + Txt_DataNascimento.Text + "','" + Txt_DataEntrada.Text + "', '" + Txt_Endereco.Text + "','" + Txt_Numero.Text + "', '" + Txt_Celular.Text + "','" + Txt_Fixo.Text + "', '" + Txt_Email.Text + "')";
            ExecuteQuery(txtQuery);
            LoadData();
            MessageBox.Show("Item inserido no Banco de Dados.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Txt_ID.Text = String.Empty;
            Txt_Nome.Text = String.Empty;
            Txt_CPF.Text = String.Empty;
            Txt_RG.Text = String.Empty;
            Txt_DataNascimento.Text = String.Empty;
            Txt_DataEntrada.Text = String.Empty;
            Txt_Endereco.Text = String.Empty;
            Txt_Numero.Text = String.Empty;
            Txt_Celular.Text = String.Empty;
            Txt_Fixo.Text = String.Empty;
            Txt_Email.Text = String.Empty;
        }

        private void Btn_Editar_Click(object sender, EventArgs e)
        {
            string txtQuery = "update MotorradTB_FuncionariosCadastro set(Nome, CPF, RG, DataNascimento, DataEntrada, Endereco, Numero, Celular, Fixo, Email) = ('" + Txt_Nome.Text + "','" + Txt_CPF.Text + "','" + Txt_RG.Text + "','" + Txt_DataNascimento.Text + "','" + Txt_DataEntrada.Text + "', '" + Txt_Endereco.Text + "','" + Txt_Numero.Text + "', '" + Txt_Celular.Text + "','" + Txt_Fixo.Text + "', '" + Txt_Email.Text + "') where ID= '" + Txt_ID.Text + "'";
            ExecuteQuery(txtQuery);
            LoadData();
            MessageBox.Show("Item Editado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Txt_ID.Text = String.Empty;
            Txt_Nome.Text = String.Empty;
            Txt_CPF.Text = String.Empty;
            Txt_RG.Text = String.Empty;
            Txt_DataNascimento.Text = String.Empty;
            Txt_DataEntrada.Text = String.Empty;
            Txt_Endereco.Text = String.Empty;
            Txt_Numero.Text = String.Empty;
            Txt_Celular.Text = String.Empty;
            Txt_Fixo.Text = String.Empty;
            Txt_Email.Text = String.Empty;
        }

        private void Btn_Deletar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo deletar o item?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                String txtQuery = "delete from MotorradTB_FuncionariosCadastro where ID= '" + Txt_ID.Text + "'";
                ExecuteQuery(txtQuery);
                LoadData();
                MessageBox.Show("Item Deletado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_ID.Text = String.Empty;
                Txt_Nome.Text = String.Empty;
                Txt_CPF.Text = String.Empty;
                Txt_RG.Text = String.Empty;
                Txt_DataNascimento.Text = String.Empty;
                Txt_DataEntrada.Text = String.Empty;
                Txt_Endereco.Text = String.Empty;
                Txt_Numero.Text = String.Empty;
                Txt_Celular.Text = String.Empty;
                Txt_Fixo.Text = String.Empty;
                Txt_Email.Text = String.Empty;
            }
        }

        private void Btn_Voltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Funcionarios_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DataGrid_Funcionarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DataGrid_Funcionarios.Rows[e.RowIndex];

                Txt_ID.Text = row.Cells["ID"].Value.ToString();
                Txt_Nome.Text = row.Cells["Nome"].Value.ToString();
                Txt_CPF.Text = row.Cells["CPF"].Value.ToString();
                Txt_RG.Text = row.Cells["RG"].Value.ToString();
                Txt_DataNascimento.Text = row.Cells["DataNascimento"].Value.ToString();
                Txt_DataEntrada.Text = row.Cells["DataEntrada"].Value.ToString();
                Txt_Endereco.Text = row.Cells["Endereco"].Value.ToString();
                Txt_Numero.Text = row.Cells["Numero"].Value.ToString();
                Txt_Celular.Text = row.Cells["Celular"].Value.ToString();
                Txt_Fixo.Text = row.Cells["Fixo"].Value.ToString();
                Txt_Email.Text = row.Cells["Email"].Value.ToString();

            }
        }
    }
}
