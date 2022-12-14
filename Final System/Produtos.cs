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
    public partial class Produtos : Form
    {
        public Produtos()
        {
            InitializeComponent();
            ArredondaCantosdoForm();
            DisplayValue();
        }
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
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
            string CommandText = "select * from MotorradTB_Produtos";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            DataGrid_Produtos.DataSource = DT;
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

        private void Produtos_MouseUp(object sender, MouseEventArgs e)
        {
            Arrastando = false;
        }

        private void Produtos_MouseDown(object sender, MouseEventArgs e)
        {
            Arrastando = true;
            ArrastarCursor = Cursor.Position;
            ArrastarForm = this.Location;
        }

        private void Produtos_MouseMove(object sender, MouseEventArgs e)
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
            string txtQuery = "insert into MotorradTB_Produtos(ID, Nome, Marca, Modelo, Preco, Categoria, Quantidade)Values('" + Txt_ID.Text + "','" + Txt_Nome.Text + "','" + Txt_Marca.Text + "','" + Txt_Modelo.Text + "','" + Txt_Preco.Text + "','" + Txt_Categoria.Text + "', '" + Txt_Quantidade.Text + "')";
            ExecuteQuery(txtQuery);
            LoadData();
            MessageBox.Show("Item inserido no Banco de Dados.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Txt_ID.Text = String.Empty;
            Txt_Nome.Text = String.Empty;
            Txt_Marca.Text = String.Empty;
            Txt_Modelo.Text = String.Empty;
            Txt_Preco.Text = String.Empty;
            Txt_Categoria.Text = String.Empty;
            Txt_Quantidade.Text = String.Empty;
        }

        private void Btn_Editar_Click(object sender, EventArgs e)
        {
            string txtQuery = "update MotorradTB_Produtos set(Nome, Marca, Modelo, Preco, Categoria, Quantidade) = ('" + Txt_Nome.Text + "','" + Txt_Marca.Text + "','" + Txt_Modelo.Text + "','" + Txt_Preco.Text + "','" + Txt_Categoria.Text + "', '" + Txt_Quantidade.Text + "')where ID= '" + Txt_ID.Text + "'";
            ExecuteQuery(txtQuery);
            LoadData();
            MessageBox.Show("Item Editado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Txt_ID.Text = String.Empty;
            Txt_Nome.Text = String.Empty;
            Txt_Marca.Text = String.Empty;
            Txt_Modelo.Text = String.Empty;
            Txt_Preco.Text = String.Empty;
            Txt_Categoria.Text = String.Empty;
            Txt_Quantidade.Text = String.Empty;
        }

        private void Btn_Deletar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo deletar o item?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                String txtQuery = "delete from MotorradTB_Produtos where ID= '" + Txt_ID.Text + "'";
                ExecuteQuery(txtQuery);
                LoadData();
                MessageBox.Show("Item Deletado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Txt_ID.Text = String.Empty;
                Txt_Nome.Text = String.Empty;
                Txt_Marca.Text = String.Empty;
                Txt_Modelo.Text = String.Empty;
                Txt_Preco.Text = String.Empty;
                Txt_Categoria.Text = String.Empty;
                Txt_Quantidade.Text = String.Empty;
            }
        }
        SQLiteDataAdapter ADA;
        SQLiteConnection Con = new SQLiteConnection("Data Source=MotorradDB.db;Version=3;New=False;Compress=true;");
        public void DisplayValue()
        {
            
            DataTable DT;

            Con.Open();

            ADA = new SQLiteDataAdapter("SELECT * from MotorradTB_Produtos", Con);
            DT = new DataTable();
            ADA.Fill(DT);
            DataGrid_Produtos.DataSource = DT;
            Con.Close();
        }
        private void Txt_Pesquisar_TextChanged(object sender, EventArgs e)
        {
            SearchData(Txt_Pesquisar.Text);
            
        }
        public void SearchData(string search)
        {
            string query = "SELECT * FROM MotorradTB_Produtos WHERE Nome like '%" + search + "%'";
            ADA = new SQLiteDataAdapter(query, Con);
            DT = new DataTable();
            ADA.Fill(DT);
            DataGrid_Produtos.DataSource = DT;
            Con.Close();
        }

        private void Produtos_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void Btn_Voltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DataGrid_Produtos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DataGrid_Produtos.Rows[e.RowIndex];

                Txt_ID.Text = row.Cells["ID"].Value.ToString();
                Txt_Nome.Text = row.Cells["Nome"].Value.ToString();
                Txt_Marca.Text = row.Cells["Marca"].Value.ToString();
                Txt_Modelo.Text = row.Cells["Modelo"].Value.ToString();
                Txt_Preco.Text = row.Cells["Preco"].Value.ToString();
                Txt_Categoria.Text = row.Cells["Categoria"].Value.ToString();
                Txt_Quantidade.Text = row.Cells["Quantidade"].Value.ToString();

            }
        }

        private void Txt_Nome_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }
    }
}
