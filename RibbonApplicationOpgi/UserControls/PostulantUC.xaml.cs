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
using RibbonApplicationOpgi.Windows;
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
                var query = from q in FiltreEntities.Postulants select q;
                if (query.Any())
                    gridControl.ItemsSource = GetDataPostulant(query);
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
            dataTable.Columns.Add("Reference", typeof(string));
            dataTable.Columns.Add("Valide", typeof(string));
            dataTable.Columns.Add("DateDemande", typeof(DateTime));
            dataTable.Columns.Add("Nom", typeof(string));
            dataTable.Columns.Add("prenom", typeof(string));
            dataTable.Columns.Add("Programme", typeof(int));

            foreach (var VARIABLE in query)
            {
                var row = dataTable.NewRow();
                row["Id"] = VARIABLE.Id;
                row["Reference"] = VARIABLE.Ref;
                row["Valide"] = VARIABLE.Valide;
                row["DateDemande"] = VARIABLE.Date_Demande;
                row["Nom"] = VARIABLE.nom;
                row["prenom"] = VARIABLE.Prenom;
                row["Programme"] = VARIABLE.Id_programme;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {
            ProgrammeWind _ProgrammeWind = new ProgrammeWind();
            _ProgrammeWind.ShowDialog();
            ChargerProgrammesList();
        }

        public void UpdatePostulant()
        {
            Postulant postulant = new Postulant();
            var selRow = gridControl.View.FocusedRowHandle;
            if (selRow > 0)
            {
                int id = Convert.ToInt32(gridControl.GetFocusedRowCellValue("Id").ToString());
                var query = from q in FiltreEntities.Postulants
                    where q.Id == id
                    select q;
                if (query.Any())
                {
                    postulant = query.FirstOrDefault();

                    /*PostulantUpdateWind postulantUpdateWind = new PostulantUpdateWind();
                    postulantUpdateWind.ShowDialog(postulant);*/

                    gridControl.View.FocusedRowHandle = selRow - 1;
                    ChargementGridControl();
                }
            }
        }

        public void DeletePostulant()
        {
            try
            {
                Postulant postulant = new Postulant();

                DialogResult messageBoxResult = MessageBox.Show("Voulez-vous vraiment supprimer ce postulant?", "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (messageBoxResult == System.Windows.Forms.DialogResult.Yes)
                {

                    var selRow = gridControl.View.FocusedRowHandle;
                    if (selRow > 0)
                    {
                        int id = Convert.ToInt32(gridControl.GetFocusedRowCellValue("Id").ToString());
                        var query = from q in FiltreEntities.Postulants
                            where q.Id == id
                            select q;
                        if (query.Any())
                        {
                            postulant = query.FirstOrDefault();
                            FiltreEntities.Postulants.Remove(postulant);
                            FiltreEntities.SaveChanges();

                            gridControl.View.FocusedRowHandle = selRow - 1;
                            ChargementGridControl();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message + "\n" + exp.InnerException, "Erreur de Suppression du Postulant",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
