using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using RibbonApplicationOpgi.Model;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace RibbonApplicationOpgi.UserControls
{
    /// <summary>
    /// Interaction logic for PostulantUC.xaml
    /// </summary>
    public partial class PostulantUC : UserControl
    {
        private FiltreEntities FiltreEntities = new FiltreEntities();
        private string operation = "";

        public PostulantUC()
        {
            InitializeComponent();
            ChargerProgrammesList();
            ChargementGridControl();
            //ChargementLayoutControl();
        }

        private void ChargerProgrammesList()
        {
            try
            {
                var query = from q in FiltreEntities.Programmes select q;
                if (query.Any())
                {
                    ProgrammeListItem.ItemsSource = GetDataProgramme(query);
                    ProgrammeListItem.ValueMember = "Id";
                    ProgrammeListItem.DisplayMember = "Designation";
                }
                    
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message + "\n" + exp.InnerException, "Erreur de chergement des Programmes",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetDataProgramme(IQueryable<Programme> query)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Designation", typeof(string));

            foreach (var VARIABLE in query)
            {
                var row = dataTable.NewRow();
                row["Id"] = VARIABLE.Id;
                row["Designation"] = VARIABLE.DesignationProgramme;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }


        private void ChargementGridControl()
        {
            try
            {
                /*var query = from q in FiltreEntities.Postulants select q;
                if (query.Any())
                    gridControl.ItemsSource = GetDataPostulant(query);*/
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message + "\n" + exp.InnerException, "Erreur de chergement des Postulants",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetDataPostulant(IQueryable<Postulant> query)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Designation", typeof(string));

            foreach (var VARIABLE in query)
            {
                var row = dataTable.NewRow();
                row["Id"] = VARIABLE.Id;
               // row["Designation"] = VARIABLE.DesignationProgramme;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

    }
}
