using LinqToSqlDemoApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToSqlDemoApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /* CRUD STARTS HERE */


        // Add new record
        private void AddNewRecord(People data)
        {
            using (LinqToSqlDemoAppDataContext db = new LinqToSqlDemoAppDataContext())
            {
                try
                {

                    db.Peoples.InsertOnSubmit(data);
                    db.SubmitChanges();
                    MessageBox.Show("Added Successfully!");

                    UpdateGrid();

                    ClearTxtBoxes();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Something is wrong" + error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
        private void btn_add_Click(object sender, EventArgs e)
        {

            var data = new People()
            {
                FirstName = tb_fname.Text,
                LastName = tb_lname.Text
            };

            AddNewRecord(data);
        }


        // Update records
        private void SaveChanges(int id)
        {
            using (LinqToSqlDemoAppDataContext db = new LinqToSqlDemoAppDataContext())
            {
                try
                {

                    if (MessageBox.Show("You sure you want to edit this?", "Make sure", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        var Changes = db.Peoples.SingleOrDefault(x => x.PeopleID == id);


                        Changes.FirstName = tb_fname.Text;
                        Changes.LastName = tb_lname.Text;

                        db.SubmitChanges();

                        MessageBox.Show("Changes saved", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        UpdateGrid();

                    }
                    else
                    {
                        MessageBox.Show("Cancelled", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }


                catch (Exception error)
                {

                    MessageBox.Show("Changes saved" + error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tb_id.Text);
            if (id > 0)
            {
                SaveChanges(id);
            }
            else
            {
                MessageBox.Show("There is no ID passed");
            }
        }


        // Display data
        private void DisplayData()
        {

            using (LinqToSqlDemoAppDataContext lq = new LinqToSqlDemoAppDataContext())
            {

                int gridCount = dataGridView1.Rows.Count;

                if (gridCount < 0)
                {
                    MessageBox.Show("There is no data available", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        var query = from fname in lq.Peoples
                                    orderby fname.FirstName descending
                                    select fname;

                        dataGridView1.DataSource = query;
                        dataGridView1.Refresh();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Something went wrong " + error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }

                }

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

       
        // Delete record
        private void DeleteRecord(int id)
        {
            using (LinqToSqlDemoAppDataContext db = new LinqToSqlDemoAppDataContext()) 
            { 
                try
                {
                    if (MessageBox.Show("You sure you want to delete this? ", "Cannot be undo", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                    {
                        var DeleteID = db.Peoples.SingleOrDefault(x => x.PeopleID == id);

                        db.Peoples.DeleteOnSubmit(DeleteID);
                        db.SubmitChanges();

                        MessageBox.Show("Deleted Successfully", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        UpdateGrid();
                    }
                    else
                    {
                        MessageBox.Show("Cancelling Deletion..");
                    }
                }
                catch (Exception error)
                {

                    MessageBox.Show("Something is wrong" + error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(tb_id.Text);

            DeleteRecord(id);
        }
     

        /* CRUD ENDS HERE */

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tb_id.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();


        }
        private void UpdateGrid()
        {


            DisplayData();
            dataGridView1.Update();
            dataGridView1.Refresh();
        }
        private void ClearTxtBoxes()
        {
            tb_id.Clear();
            tb_fname.Clear();
            tb_lname.Clear();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tb_id.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tb_fname.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            tb_lname.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString(); 
        }
    }
}
