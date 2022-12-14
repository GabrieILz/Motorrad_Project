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
    public partial class Caixa : Form
    {
        public Caixa()
        {
            InitializeComponent();
            ArredondaCantosdoForm();
        }
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private DataSet DS2 = new DataSet();
        private DataTable DT2 = new DataTable();


        private void SetConnection()
        {
            sql_con = new SQLiteConnection
                ("Data Source=MotorradDB.db;Version=3;New=False;Compress=true;");
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
        private void LoadData2()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select * from MotorradTB_Carrinho";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS2.Reset();
            DB.Fill(DS2);
            DT2 = DS2.Tables[0];
            DataGrid_Carrinho.DataSource = DT2;
            sql_con.Close();
        }

        private void Caixa_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadData2();
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

                Txt_Quantidade.Text = "1";
            }
        }

        private void Btn_Adicionar_Click(object sender, EventArgs e)
        {
            string txtQuery = "insert into MotorradTB_Carrinho(ID, Nome, Marca, Modelo, Preco, Quantidade)Values('" + Txt_ID.Text + "','" + Txt_Nome.Text + "','" + Txt_Marca.Text + "','" + Txt_Modelo.Text + "','" + Txt_Preco.Text + "','" + Txt_Quantidade.Text + "')";
            ExecuteQuery(txtQuery);
            LoadData2();
            double valorTotal = (Convert.ToDouble(Txt_Preco.Text) * Convert.ToDouble(Txt_Quantidade.Text)) + Convert.ToDouble(Txt_ValorTotal.Text);
            Txt_ValorTotal.Text = valorTotal.ToString();
        }

        private void Btn_Finalizar_Click(object sender, EventArgs e)
        {
            SetConnection();



            if (MessageBox.Show("Fizalizar compra?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                sql_con.Open();

                string Querry = "DELETE from MotorradTB_Carrinho";
                SQLiteCommand CMD = new SQLiteCommand(Querry, sql_con);
                CMD.ExecuteNonQuery();
                DS2.Reset();
                DB.Fill(DS2);
                DataGrid_Carrinho.DataSource = DT2;
                sql_con.Close();
                LoadData();
                LoadData2();
                Txt_ValorTotal.Text = "0";

                MessageBox.Show("Compra Realizada.", "Sucesso!", MessageBoxButtons.OK);
            }
        }
        Point ArrastarCursor;
        Point ArrastarForm;
        bool Arrastando;

        private void Btn_Voltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Caixa_MouseUp(object sender, MouseEventArgs e)
        {
            Arrastando = false;
        }

        private void Caixa_MouseDown(object sender, MouseEventArgs e)
        {
            Arrastando = true;
            ArrastarCursor = Cursor.Position;
            ArrastarForm = this.Location;
        }

        private void Caixa_MouseMove(object sender, MouseEventArgs e)
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
    }
}
