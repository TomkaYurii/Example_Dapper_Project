using Dapper_Example.DAL.Repositories.Interfaces;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly IUnitOfWork _unitOfWork;
        public Form1(IUnitOfWork uow)
        {
            InitializeComponent();
            _unitOfWork = uow;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label9.Hide();
                label9.Text = "";


                int id = Convert.ToInt32(textBox7.Text);
                var product = await _unitOfWork._productRepository.GetAsync(id);

                textBox1.Text = product.Name;
                textBox2.Text = product.Properties;
                textBox3.Text = product.Price.ToString();
                textBox4.Text = product.Seller;
                textBox5.Text = product.Brand;

                var category_of_product = await _unitOfWork._categoryRepository.GetAsync(product.Id);

                textBox6.Text = category_of_product.Name;
            }
            catch (Exception ex)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                label9.Show();
                label9.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}