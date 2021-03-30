using System;
using System.IO;
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
                var dbName = "BurgerDB.db";

                if (File.Exists(dbName))
                {
                    File.Delete(dbName);
                }
                    
                await using var dbContext = new BurgerContext();
                await dbContext.Database.EnsureCreatedAsync();

                if(txtName.Text != "" && txtPrice.Text != "")
                {
                    await dbContext.Burgers.AddRangeAsync(
                    new Burger() { BurgerName = txtName.Text, BurgerPrice = Convert.ToDouble(txtPrice.Text) });
                    await dbContext.SaveChangesAsync();
                    MessageBox.Show("Data is saved", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("Name either Price is empty", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                txtName.Clear();
                txtPrice.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void btnRead_Click(object sender, EventArgs e)
        {
            txtBurger.Clear();

            await using var dbContext = new BurgerContext();

            dbContext.Burgers.ToList().ForEach(p =>
            {
                txtBurger.AppendText($"Burger's Name: {p.BurgerName}; Price: {p.BurgerPrice:C};" + Environment.NewLine);
            });
        }
    }
}