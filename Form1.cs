using System;
using System.Linq;
using System.Windows.Forms;


namespace Prokerka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                await using var dbContext = new BurgerContext();
                await dbContext.Database.EnsureCreatedAsync();  
   
                if (!String.IsNullOrEmpty(txtName.Text) && !String.IsNullOrEmpty(txtPrice.Text))
                {
                    await dbContext.Burgers.AddRangeAsync(
                    new Burger() { Name = txtName.Text, Price = Convert.ToDouble(txtPrice.Text) });
                    await dbContext.SaveChangesAsync();
                    MessageBox.Show("Data is saved", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                               
                else
                {
                    MessageBox.Show("Something went wrong", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                txtName.Clear();
                txtPrice.Clear();
            }
        }
        
        private async void btnRead_Click(object sender, EventArgs e)
        {
            txtBurger.Clear();

            await using var dbContext = new BurgerContext();

            try 
            {
                dbContext.Burgers.ToList().ForEach(p =>
                {
                    txtBurger.AppendText($"Burger's Name: {p.Name}; Price: {p.Price:C};" + Environment.NewLine);
                });
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                txtName.Clear();
                txtPrice.Clear();
            }
        }
    }
}