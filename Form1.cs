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
            this.amountAsTextTextBox.ResetText();


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
            double valueAsDouble;
            //Check we are indeed handling a number
            if (double.TryParse(value, out valueAsDouble))
            {
                //Format the text as currency
                this.monto.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", valueAsDouble).Replace("$", "");
                this.amountAsTextTextBox.Text = CapitalizeFirstLetter(VCAutoRepairPaymentNotes.CurrencyTranslatorFromNumbersToText.ConvertDoubleToCurrencyText(valueAsDouble));
                if (string.IsNullOrEmpty(this.amountAsTextTextBox.Text))
                {
                    this.amountAsTextTextBox.Focus();
                }
            }
            else
            {
                if(this.monto.Text != null && this.monto.Text.Length > 0)
                {
                    this.amountAsTextTextBox.ResetText();
                    this.monto.Select(0, this.monto.Text.Length);
                    this.monto.Focus();
                }
              
            }


            

            this.refreshSummaryDueToTextChange(sender, e);
        }


        string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input; // Return the original string if it's empty or null
            }

            // Capitalize the first letter and concatenate the rest in lowercase
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }

    public static class CurrencyTranslatorFromNumbersToText
    {

        public static string[] BELOW_TWENTY_ARRAY = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

        public static string[] TENS_ARRRAY = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        
        public static string ConvertDoubleToCurrencyText(double amount)
        {

            bool isNegative = amount < 0;
            if (isNegative)
            {
                amount = Math.Abs(amount);
            }


            if(amount > 99999)
            {
                MessageBox.Show("Tocayo\n\nFor this amount..I'm not even going to worry about translating it to text... enjoy typing it!", "PIMP Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }


            long dollars = (long)amount;
            int cents = (int)Math.Round((amount - dollars) * 100);

            

            string dollarsText = ConvertToText(dollars);
            string centsText = ConvertToText(cents);

            if (dollars == 0)
            {
                dollarsText = "Zero";
            }

            if(cents == 0)
            {
                centsText = "Zero";
            }

            if (isNegative)
            {
                return $"Negative {dollarsText} Dollars and {centsText} Cents";
            }
            else
            {
                return $"{dollarsText} Dollars and {centsText} Cents";
            }
        }



        /*
         * 
         * TODO: Fix this method, its not readable and add proper documentation to it
         */
        static string ConvertToText(long value)
        {
            
            if (value/20000 >= 1)
            {
                string addS = "";
                if (value / 20000 > 1)
                {
                    addS = "s";
                }

                //TODO: FIX this logic and variable names
                long tens_array_value = value / 10000;
                long belowTwentyValue = (value/1000) % 10;
                Console.WriteLine("tens_array_value = " + tens_array_value);
                Console.WriteLine("belowTwentyValue = " + belowTwentyValue);

                return TENS_ARRRAY[value / 10000] + " " + BELOW_TWENTY_ARRAY[belowTwentyValue] + " Thousand" + addS + " " + ConvertToText(value % 1000);

            }
            else if(value/1000 >= 1)
            {
                string addS = "";
                if (value / 1000 > 1)
                {
                    addS = "s";
                }
                return BELOW_TWENTY_ARRAY[value / 1000] + " Thousand" + addS + " " + ConvertToText(value % 1000);
            }
            else if(value/100 >= 1)
            {
                string addS = "";
                if (value / 100 > 1) {
                    addS = "s";
                }
                return BELOW_TWENTY_ARRAY[value / 100] + " Hundred" + addS + " " + ConvertToText(value % 100);
            }else if(value/20 >= 1)
            {
                return TENS_ARRRAY[value/ 10] + " " + ConvertToText(value%10);
            }

            Console.WriteLine("value = " + value);

            //below 20
            return BELOW_TWENTY_ARRAY[value];

        }
    }

}
