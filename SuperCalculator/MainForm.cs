namespace SuperCalculator
{
    public partial class MainForm : Form
    {
        // Code by Hok Ki Chan, Spring 2022 Cohort
        
        //Creation of new instances
        
        private BMIRCalculator bmir = new BMIRCalculator();

        private SavingCalculator savingCalc = new SavingCalculator();
        
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
        }

        #region Read input for BMI and BMR Calculators

        // Method to read height inputted by user with validation
        private bool ReadHeight()
        {
            double height = 0.0;

            double inch = 0.0;

            bool ok = true;

            // Return appropriate values for metric and imperial units
            if (bmir.GetUnitType() == UnitTypes.Imperial)
            {
                ok = double.TryParse(txtCmFt.Text.Trim(), out height);
                if (!ok)
                    MessageBox.Show("The ft value is invalid");

                ok = double.TryParse(txtInch.Text.Trim(), out inch);
                if (!ok)
                    MessageBox.Show("The inch value is invalid");
                height = height * 12.0 + inch;
            }
            else
            {
                ok = double.TryParse(txtCmFt.Text.Trim(), out height);
                if (!ok)
                    MessageBox.Show("The height value is invalid");
                height = height / 100.0;
            }
            
            // Use of setter to set height with valid values
            if (height > 0.0)
            {
                bmir.SetHeight(height);
            } else
            {
                MessageBox.Show("Height value should not be zero or negative!");
                ok = false;
            }
            return ok;
        }

        // Method to read weight inputted by user with validation
        private bool ReadWeight()
        {
            bool ok = true;

            double weight = 0.0;

            ok = double.TryParse(txtWeight.Text.Trim(), out weight);
            if (ok && weight >0)
            {
                bmir.SetWeight(weight);
            }
            else
            {
                MessageBox.Show("Invalid weight value!");
                ok = false;
            }
            return ok;
        }

        // Validate input if both height and weight are valid
        private bool ReadInput()
        {
            return ReadHeight() && ReadWeight();
        }

        // Read name inputted by user
        private string ReadName()
        {
            string name;
            name = txtName.Text.Trim();

            // Set name to Unknown for blank user input
            if (string.IsNullOrEmpty(name))
            {
                name = "Unknown";
            }
            return name;
        }
        #endregion

        #region Code for GUI and update of displays

        // Actions for clicking the Calculate BMI button
        private void btnCalBMI_Click(object sender, EventArgs e)
        {
            // Update name display
            gbxBMIResult.Text = "Results for " + ReadName();

            // Calculate BMI and display with valid values
            bool ok = ReadInput();

            if (ok)
            {
                double convertedAmount = bmir.CalculateBMI();
                lblBMIDisplay.Text = convertedAmount.ToString("0.00");
                lblWeightCat.Text = bmir.BMIWeightCategory();

                string normalWeightUpper = bmir.GetNormalWelghtUpper().ToString("0.0");

                string normalWeightLower = bmir.GetNormalWelghtLower().ToString("0.0");

                if (bmir.GetUnitType() == UnitTypes.Imperial)
                    lblNormalWeight.Text = "Normal weight should be between " + normalWeightLower + " and " + normalWeightUpper + " lbs";
                else
                    lblNormalWeight.Text = "Normal weight should be between " + normalWeightLower + " and " + normalWeightUpper + " kg";
            }
        }

        // Method to read unit chosen
        private void ReadUnit()
        {
            if (rbtnMetric.Checked)
                bmir.SetUnitType(UnitTypes.Metric);
            else
                bmir.SetUnitType(UnitTypes.Imperial);     
        }

        // Initialization of GUI
        private void InitializeGUI()
        {
            this.Text += " by Hokki Chan";
            rbtnMetric.Checked = true;
            rbtnFemale.Checked = true;
            rbtnGrp0.Checked = true;
            lblNormalBMI.Text = "Normal BMI is between 18.5 and 24.9";
            lblNormalWeight.Text = string.Empty;
            lblAmountPaid.Text = string.Empty;
            lblAmountEarned.Text = string.Empty;
            lblFinalBal.Text = string.Empty;
            lblTotalFees.Text = string.Empty;
        }

        // Update unit displays
        private void UpdatedHeightText()
        {
            if (rbtnMetric.Checked)
            {
                lblHeight.Text = "Height (cm)";
                txtInch.Visible = false;
            } 
            else
            {
                lblHeight.Text = "Height (ft, in)";
                txtInch.Visible = true;
            }
            lblWeightCat.Text = "";
            lblBMIDisplay.Text = "";
            txtCmFt.Text = "";
            txtInch.Text = "";
        }

        private void UpdatedWeightText()
        {
            if (rbtnMetric.Checked)
            {
                lblWeight.Text = "Weight (kg)";
            } 
            else
            {
                lblWeight.Text = "Weight (lb)";
            }
            lblWeightCat.Text = "";
            lblBMIDisplay.Text = "";
            txtWeight.Text = "";
        }

        // Actions of for unit buttons clicked
        private void rbtnMetric_CheckedChanged(object sender, EventArgs e)
        {
            UpdatedHeightText();
            UpdatedWeightText();
            ReadUnit();
        }

        private void rbtnImperial_CheckedChanged(object sender, EventArgs e)
        {
            UpdatedHeightText();
            UpdatedWeightText();
            ReadUnit();
        }
        #endregion

        // General method for reading and validating double values
        private double ReadDouble(String str, out bool success)
        {
            double value = -1.0;
            success = false;
            if (double.TryParse(str, out value))
                success = true;
            return value;
        }

        #region Saving Calculator
        private bool ReadSavingInput()
        {
            bool success = true;

            // Validation for initial deposit value
            double initialDeposit = ReadDouble(txtInitialDeposit.Text, out success);
            if (success)
            {
                savingCalc.SetInitialDeposit(initialDeposit);
            }
            else
            {
                MessageBox.Show("Invalid value for initial deposit!");
                success = false;
            }

            // Validation for monthly deposit value
            double monthlyDeposit = ReadDouble(txtDeposit.Text, out success);
            if (success)
            {
                savingCalc.SetMonthlySaving(monthlyDeposit);
            }
            else
            {
                MessageBox.Show("Invalid value for monthly deposit!");
                success = false;
            }
            success = int.TryParse(txtPeriod.Text.Trim(), out int period);
            if (success)
                savingCalc.SetPeriod(period);
            else
                MessageBox.Show("Invalid value for period!");

            // Validation for growth rate
            double growthRate = ReadDouble(txtGrowthPercent.Text, out success);
            if (success)
            {
                savingCalc.SetGrowthRate(growthRate);
            }
            else
            {
                MessageBox.Show("Invalid value for growth rate!");
                success = false;
            }

            // Validation for fee rate
            double feeRate = ReadDouble(txtFeePercent.Text, out success);
            if (success)
            {
                savingCalc.SetFeeRate(feeRate);
            }
            else
            {
                MessageBox.Show("Invalid value for fee rate!");
                success = false;
            }
            return success;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterToScreen();
        }

        // Action button for Savings Calculator
        private void btnCalSavings_Click(object sender, EventArgs e)
        {
            if (ReadSavingInput() == true)
            {
                savingCalc.CalculateSavings();
                double amountEarned = savingCalc.CalculateSavings() - savingCalc.CalculateAmountPaid();
                double totalFees = savingCalc.CalculateGrossTotal() - savingCalc.CalculateSavings();

                lblAmountPaid.Text = savingCalc.CalculateAmountPaid().ToString("0.00");
                lblAmountEarned.Text = amountEarned.ToString("0.00");
                lblFinalBal.Text = savingCalc.CalculateSavings().ToString("0.00");
                lblTotalFees.Text = totalFees.ToString("0.00");
            }
        }
        #endregion

        #region BMR Calculator

        // Updating value for activity level
        private double ActivityLevelMultiplier()
        {
            double multiplier;
            if (rbtnGrp0.Checked)
                multiplier = 1.2;
            else if (rbtnGrp1.Checked)
                multiplier = 1.375;
            else if (rbtnGrp2.Checked)
                multiplier = 1.550;
            else if (rbtnGrp3.Checked)
                multiplier = 1.725;
            else
                multiplier = 1.9;
            return multiplier;
        }

        // Action button for BMR calculations
        private void btnCalculateBMR_Click(object sender, EventArgs e)
        {
            ReadName();

            bool ok = ReadInput();

            ok = int.TryParse(txtAge.Text.Trim(), out int age);
            if (ok)
                bmir.SetAge(age);
            else
                MessageBox.Show("Invalid value for age!");
            
            if (ok)
            {
                ReadHeight();
                double bmr = bmir.CalculateBMR();
                
                // Converting value between genders
                if (rbtnFemale.Checked)
                {
                    bmr -= 161;
                }
                else
                {
                    bmr += 5;
                }
                lbxBMRResult.Items.Clear();
                lbxBMRResult.Items.Add("BMR Results for " + ReadName());
                lbxBMRResult.Items.Add("Your BMR (calories/day)\t\t" + bmr.ToString("0.0"));

                // Calculate calorie values for various weight loss
                double bmrMaintain = bmr * ActivityLevelMultiplier();
                double bmrLose500g = bmr * ActivityLevelMultiplier() - 500 ;
                double bmrLose1kg = bmr * ActivityLevelMultiplier() - 1000;
                double bmrGain500g = bmr * ActivityLevelMultiplier() + 500;
                double bmrGain1kg = bmr * ActivityLevelMultiplier() + 1000;

                // Displaying values to list box
                lbxBMRResult.Items.Add("Calories to maintain your weight\t"+ bmrMaintain.ToString("0.0"));
                lbxBMRResult.Items.Add("Calories to lose 0.5 kg per week\t" + bmrLose500g.ToString("0.0"));
                lbxBMRResult.Items.Add("Calories to lose 1 kg per week\t" + bmrLose1kg.ToString("0.0"));
                lbxBMRResult.Items.Add("Calories to gain 0.5 kg per week\t" + bmrGain500g.ToString("0.0"));
                lbxBMRResult.Items.Add("Calories to gain 1 kg per week\t" + bmrGain1kg.ToString("0.0"));
                lbxBMRResult.Items.Add("");
                lbxBMRResult.Items.Add("Losing more than 1,000 calories per day is to");
                lbxBMRResult.Items.Add("be avoided.");
            }
        }
        #endregion
    }
}