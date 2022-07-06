using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using RibbonApplicationOpgi.UserControls;
using RibbonApplicationOpgi.Windows;

namespace RibbonApplicationOpgi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        private PostulantUC postulantUc = new PostulantUC();
        private BenificiereUC benificiereUc = new BenificiereUC();
        private StatistiquesUC statistiquesUc = new StatistiquesUC();

        public MainWindow()
        {
            InitializeComponent();

            PostulantTab.Content = postulantUc;
            BenificierTab.Content = benificiereUc;
            StatistiquesTab.Content = statistiquesUc;
        }

        private void ribbonControl_SelectedPageChanged(object sender, DevExpress.Xpf.Ribbon.RibbonPropertyChangedEventArgs e)
        {
            if (ribbonControl.SelectedPage == PostulantRibbonPage)
            {
                tabControl.SelectedItem = PostulantTab;
            }
            if (ribbonControl.SelectedPage == BenificierRibbonPage)
            {
                tabControl.SelectedItem = BenificierTab;
            }
            if (ribbonControl.SelectedPage == ProjetRibbonPage)
            {
                tabControl.SelectedItem = ProjetTab;
            }
            if (ribbonControl.SelectedPage == ReglageRibbonPage)
            {
                tabControl.SelectedItem = ReglageTab;
            }
            if (ribbonControl.SelectedPage == StatistiquesRibbonPage)
            {
                statistiquesUc.DashboardControl.DashboardSource = "DashBoard.xml";
                tabControl.SelectedItem = StatistiquesTab;
            }
        }


        #region Postulant

        private void PostulantUpdateBtn_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            postulantUc.UpdatePostulant();
        }

        private void PostulantDeleteBtn_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            postulantUc.DeletePostulant();
        }

        private void PostulantXlsxBtn_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Document Excel (*.xlsx)|*.xlsx";
            dialog.DefaultExt = ".xlsx";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    postulantUc.gridControl.View.ExportToXlsx(dialog.FileName);
                    MessageBox.Show("Exportation effectuée avec succès ", "Succès!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Exportation impossible: " + exp.Message, "Erreur!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                dialog.Dispose();
            }
        }

        private void PostulantPdfBtn_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Document Excel (*.pdf)|*.pdf";
            dialog.DefaultExt = ".pdf";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    postulantUc.gridControl.View.ExportToPdf(dialog.FileName);
                    MessageBox.Show("Exportation effectuée avec succès ", "Succès!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Exportation impossible: " + exp.Message, "Erreur!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                dialog.Dispose();
            }
        }

        private void PostulantPrintPreviewBtn_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            postulantUc.gridControl.View.ShowRibbonPrintPreview(this, "Nom du document Test", "Titre Test");
        }

        private void PostulantPrintBtn_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            //postulantUc.gridControl.View.Print();
        }
        
        #endregion

        #region Benificièr



        #endregion

        #region Projet

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            ProgrammeWind programmeWind = new ProgrammeWind();
            programmeWind.ShowDialog();
        }

        #endregion

        #region Statistiques
        private void DashBoardPrintBtn_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            statistiquesUc.DashboardControl.ShowPrintPreview();
        }

        private void DashBoard1Btn_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            statistiquesUc.DashboardControl.DashboardSource = "DashBoard.xml";
        }

        private void DashBoard2Btn_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            statistiquesUc.DashboardControl.DashboardSource = "DashBoard2.xml";
        }



        #endregion

        
    }
}
