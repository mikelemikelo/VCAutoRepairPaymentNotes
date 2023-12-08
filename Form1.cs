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
        private static Font AMOUNT_AS_TEXT_FONT = new Font("Arial", 10, FontStyle.Italic);
        private static Font SUMMARY_PRINT_FONT = new Font("Arial", 09);
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
            float yPosition = e.MarginBounds.Top + 18;
            float oneThirdOfPageHeight = e.PageBounds.Height / 3;


            float checkDateYPosition = e.MarginBounds.Top / 3;
            float summaryXPosition = e.MarginBounds.Left / 3;



            //Date field
            e.Graphics.DrawString(this.checkDateTextBox.Text, Form1.DEFAULT_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, xPosition + 540, checkDateYPosition);

            //Row 1
            e.Graphics.DrawString(this.checkBody.Text, Form1.DEFAULT_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, xPosition, yPosition);
            e.Graphics.DrawString(this.monto.Text, Form1.DEFAULT_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, xPosition + 580, yPosition);

            yPosition += 60;
            e.Graphics.DrawString(this.amountAsTextTextBox.Text, Form1.AMOUNT_AS_TEXT_FONT, Form1.DEFAULT_PRINT_BRUSH, xPosition , yPosition);

            yPosition += 60;
            //Row 2
            e.Graphics.DrawString(this.memo.Text, Form1.DEFAULT_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, xPosition, yPosition);

            //Summary one
            yPosition = oneThirdOfPageHeight;
            e.Graphics.DrawString(this.generateCheckSummary(), Form1.SUMMARY_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, summaryXPosition, yPosition);

            //Summary two
            yPosition += oneThirdOfPageHeight;
            e.Graphics.DrawString(this.generateCheckSummary(), Form1.SUMMARY_PRINT_FONT, Form1.DEFAULT_PRINT_BRUSH, summaryXPosition, yPosition);


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
         * Amount as text: AAAAEEEEBBBCC
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
            checkSummary.Append("Amount as text: " + this.amountAsTextTextBox.Text);
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

        private static int ONE_MILLION = 1000000;
        private static int ONE_HUNDRED_THOUSANDS = 100000;
        private static int TWENTY_THOUSANDS = 20000;
        private static int TEN_THOUSANDS = 10000;
        private static int ONE_THOUSAND = 1000;
        private static int ONE_HUNDRED = 100;


        public static string ConvertDoubleToCurrencyText(double amount)
        {

            bool isNegative = amount < 0;
            if (isNegative)
            {
                amount = Math.Abs(amount);
            }


            if(amount >= ONE_MILLION)
            {
                MessageBox.Show("Tocayo\n\nFor this amount..I'm not even going to worry about translating it to text...\nEnjoy typing it! ¯\\_(ツ)_/¯", "PIMP Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }

            long dollars = (long)amount;
            int cents = (int)Math.Round((amount - dollars) * 100);
            
            string dollarsText = convertAmountToText(dollars);
            string centsText = convertAmountToText(cents);

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
         * Method that translates an amount (long) into its equivalent as String.
         * This method is only able to translate numbers less than ONE_MILLION 
         */
        static string convertAmountToText(long amount)
        {
            if (amount >= ONE_MILLION)
            {
                //If value is equal of higher than ONE_MILLION then we don't compute the amount as text
                return "";

            }

            // For values less than ONE_MILLION 
            if (amount/ ONE_HUNDRED_THOUSANDS >=1 ) {

                string unitName = " Hundred";
                if (amount / ONE_HUNDRED_THOUSANDS > 1)
                {
                    unitName += "s";
                }
                return BELOW_TWENTY_ARRAY[amount / ONE_HUNDRED_THOUSANDS] + unitName + " " + convertAmountToText(amount % ONE_HUNDRED_THOUSANDS);

            }
            else if (amount/TWENTY_THOUSANDS >= 1)
            {
                string unitName = " Thousand";
                if (amount / ONE_THOUSAND > 1)
                {
                    unitName += "s";
                }

                //Calculate 
                long firstDigitOfThousandsAmount = (amount/ ONE_THOUSAND) % 10;
                Console.WriteLine("firstDigitOfThousandsAmount= " + firstDigitOfThousandsAmount);
                return TENS_ARRRAY[amount / TEN_THOUSANDS] + " " + BELOW_TWENTY_ARRAY[firstDigitOfThousandsAmount] + unitName + " " + convertAmountToText(amount % ONE_THOUSAND);

            }
            else if(amount/ONE_THOUSAND >= 1)
            {
                string unitName = " Thousand";
                if (amount / ONE_THOUSAND > 1)
                {
                    unitName += "s";
                }
                return BELOW_TWENTY_ARRAY[amount / ONE_THOUSAND] + unitName + " " + convertAmountToText(amount % ONE_THOUSAND);
            }
            else if(amount/ ONE_HUNDRED >= 1)
            {
                string unitName = " Hundred";
                if (amount / ONE_HUNDRED > 1) {
                    unitName += "s";
                }
                return BELOW_TWENTY_ARRAY[amount / ONE_HUNDRED] + unitName + " " + convertAmountToText(amount % ONE_HUNDRED);
            }else if(amount/20 >= 1)
            {
                return TENS_ARRRAY[amount/ 10] + convertAmountToText(amount%10);
            }

            //below 20
            return BELOW_TWENTY_ARRAY[amount];

        }
    }

}
