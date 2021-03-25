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
                    File.Delete(dbName);

                await using var dbContext = new BurgerContext();
                await dbContext.Database.EnsureCreatedAsync();
                await dbContext.Burgers.AddRangeAsync(new Burger[]
                {
                    new Burger() {BurgerName = txtName.Text, BurgerPrice = Convert.ToDouble(txtPrice.Text)}
                });

                await dbContext.SaveChangesAsync();

                MessageBox.Show("Data saved", "INFO", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private async void btnRead_Click(object sender, EventArgs e)
        {
            await using var dbContext = new BurgerContext();
            dbContext.Burgers?.ToList().ForEach(p =>
            {
                txtBurger.Text = ($"Name: {p.BurgerName} Price: {p.BurgerPrice:C}");
            });
        }
    }
}