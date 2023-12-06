using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCAutoRepairPaymentNotes
{
    public partial class Form1 : Form
    {

        private DateTime currentDateTime = DateTime.Now;
        private static String CURRENT_DATE_FORMAT = "MMMM dd,yyyy";
        private static String CURRENT_DATE_FORMAT_WITH_TIME = "MMMM dd,yyyy HH:mm tt";
        private static Regex VALIDADOR_MONTO_REGEX = new Regex(@"^(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");



        private static Font DEFAULT_PRINT_FONT = new Font("Arial", 12);
        private static SolidBrush DEFAULT_PRINT_BRUSH = new SolidBrush(Color.Black);

        public Form1()
        {
            InitializeComponent();
            resetCurrentDate();
        }

        public void resetCurrentDate()
        {
            this.checkDateTextBox.Text = this.currentDateTime.ToString(Form1.CURRENT_DATE_FORMAT);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void printButton_Click(object sender, EventArgs e)
        {

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintPageHandler;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;

            if (printDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                printDoc.Print();
            }

        }

        /**
         * Handler in charge of desgining the page and content to be printed.
         *
         *
         */
        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            float xPosition = e.MarginBounds.Left - 10;
            float yPosition = e.MarginBounds.Top + 20;
            float oneThirdOfPageHeight = e.PageBounds.Height / 3;


            float checkDateYPosition = e.MarginBounds.Top / 2;
            float summaryXPosition = e.MarginBounds.Left / 2;



            //Date field
            e.Graphics.DrawString(this.checkDateTextBox.Text, Form1.DEFAULT_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, xPosition + 580, checkDateYPosition);

            //Row 1
            e.Graphics.DrawString(this.checkBody.Text, Form1.DEFAULT_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, xPosition, yPosition);
            e.Graphics.DrawString(this.monto.Text, Form1.DEFAULT_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, xPosition + 580, yPosition);

            yPosition += 122;
            //Row 2
            e.Graphics.DrawString(this.memo.Text, Form1.DEFAULT_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, xPosition, yPosition);

            //Summary one
            yPosition = oneThirdOfPageHeight;
            e.Graphics.DrawString(this.generateCheckSummary(), Form1.DEFAULT_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, summaryXPosition, yPosition);

            //Summary two
            yPosition += oneThirdOfPageHeight;
            e.Graphics.DrawString(this.generateCheckSummary(), Form1.DEFAULT_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, summaryXPosition, yPosition);


        }



        private void refreshSummaryDueToTextChange(object sender, EventArgs e)
        {
            this.middleSummary.Text = this.generateCheckSummary();
        }


        /**
         * Method that generates the check summary and returns it as String; following the next format:
         * 
         * Printed date: format(MMMM dd,yyyy HH:mm:ss)
         * Actual date on check: format(MMMM dd,yyyy) - could be free form as user can modify it manually 
         * Ammount: $ XXXXXX
         * Pay to the order of: YYYYYYYYYY
         * Memo: ZZZZZZZ
         * ** Optional - if internal notes is present and its not empty **
         * Internal notes: AAAAAAAAAAAA
         *
         */
        private String generateCheckSummary()
        {
            StringBuilder checkSummary = new StringBuilder();
            checkSummary.Append("Printed date: " + this.currentDateTime.ToString(Form1.CURRENT_DATE_FORMAT_WITH_TIME));
            checkSummary.Append("\n");
            checkSummary.Append("Actual date on check: " + this.checkDateTextBox.Text);
            checkSummary.Append("\n");
            checkSummary.Append("Amount: $" + this.monto.Text);
            checkSummary.Append("\n");
            checkSummary.Append("Pay to the order of: " + this.checkBody.Text);
            checkSummary.Append("\n");
            checkSummary.Append("Memo: " + this.memo.Text);
            if (this.optionalInternalNotesField.Text != null && this.optionalInternalNotesField.Text.Trim().Length > 0)
            {
                checkSummary.Append("\n");
                checkSummary.Append("Internal notes: " + this.optionalInternalNotesField.Text);
            }

            return checkSummary.ToString();
        }

        private void resetFormButton_Click(object sender, EventArgs e)
        {

            this.resetCurrentDate();
            this.checkBody.ResetText();
            this.monto.ResetText();
            this.memo.ResetText();
            this.middleSummary.ResetText();
            this.optionalInternalNotesField.ResetText();
            this.monto.Text = "0.0";


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /**
         * Method that formats amount quantity to match the format $xxx,yyy.zz
         */
        private void monto_Leave(object sender, EventArgs e)
        {
            //Remove previous formatting, or the decimal check will fail including leading zeros
            string value = this.monto.Text.Replace(",", "")
                .Replace("$", "");
            decimal valueAsDecimal;
            //Check we are indeed handling a number
            if (decimal.TryParse(value, out valueAsDecimal))
            {
                //Format the text as currency
                this.monto.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", valueAsDecimal).Replace("$", "");
            }
            else
            {
                Console.WriteLine("HERE Doing this");
                if(this.monto.Text != null && this.monto.Text.Length > 0)
                {
                    this.monto.Select(0, this.monto.Text.Length);
                    this.monto.Focus();
                }
              
            }
            this.refreshSummaryDueToTextChange(sender, e);
        }


    }

}
