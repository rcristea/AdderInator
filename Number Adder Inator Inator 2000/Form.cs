using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Drawing;

namespace Number_Adder_Inator_Inator_2000 {
    public partial class AdderInator : Form {
        private string input;
        private bool firstClick;
        private List<List<double>> sums;

        public AdderInator() {
            InitializeComponent();

            this.input = "0";
            this.firstClick = true;

            labelInputNumber.Text = this.input;

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(keyDownHandler);
        }

        private void buttonZero_Click(object sender, EventArgs e) {
            checkFirstClick();
            addNumber("0");
        }

        private void buttonOne_Click(object sender, EventArgs e) {
            checkFirstClick();
            addNumber("1");
        }

        private void buttonTwo_Click(object sender, EventArgs e) {
            checkFirstClick();
            addNumber("2");
        }

        private void buttonThree_Click(object sender, EventArgs e) {
            checkFirstClick();
            addNumber("3");
        }

        private void buttonFour_Click(object sender, EventArgs e) {
            checkFirstClick();
            addNumber("4");
        }

        private void buttonFive_Click(object sender, EventArgs e) {
            checkFirstClick();
            addNumber("5");
        }

        private void buttonSix_Click(object sender, EventArgs e) {
            checkFirstClick();
            addNumber("6");
        }

        private void buttonSeven_Click(object sender, EventArgs e) {
            checkFirstClick();
            addNumber("7");
        }

        private void buttonEight_Click(object sender, EventArgs e) {
            checkFirstClick();
            addNumber("8");
        }

        private void buttonNine_Click(object sender, EventArgs e) {
            checkFirstClick();
            addNumber("9");
        }

        private void buttonPlusMinus_Click(object sender, EventArgs e) {
            double val = Double.Parse(labelInputNumber.Text);
            if (val > 0.0) {
                this.input = "-" + this.input;
                labelInputNumber.Text = this.input;
            } else {
                this.input = "" + val * -1;
                labelInputNumber.Text = this.input;
            }
        }

        private void buttonDecimal_Click(object sender, EventArgs e) {
            if (!this.input.Contains(".")) {
                this.input += ".";
                labelInputNumber.Text = this.input;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e) {
            if (textBoxTargetBalance.Text.Length != 0) {
                listViewValues.Items.Add(new ListViewItem(this.input));
                this.input = "0";
                labelInputNumber.Text = this.input;
                this.firstClick = true;

                startSum();
            } else {
                MessageBox.Show("Please enter a target balance");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e) {
            if (this.input.Length <= 1) {
                this.input = "0";
                labelInputNumber.Text = this.input;
                this.firstClick = true;
            } else if (Double.Parse(this.input) != 0) {
                this.input = this.input.Substring(0, this.input.Length - 1);
                labelInputNumber.Text = input;
            }
        }

        private void checkFirstClick() {
            if (firstClick && !this.input.Contains(".")) {
                labelInputNumber.Text = String.Empty;
                this.input = String.Empty;
                firstClick = false;
            }
        }

        private void addNumber(string number) {
            if (this.input.Length < 15) {
                this.input += number;
                labelInputNumber.Text = this.input;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e) {
            this.input = "0";
            labelInputNumber.Text = this.input;
            this.firstClick = true;
        }

        private void listViewValues_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (listViewValues.FocusedItem.Bounds.Contains(e.Location)) {
                    for (int i = 0; i < this.sums.Count; i++) {
                        if (this.sums[i].Contains(Double.Parse(listViewValues.FocusedItem.Text))) {
                            clearColor();
                        }
                    }

                    listViewValues.FocusedItem.Remove();
                }
            }
        }

        private void startSum() {
            this.sums = new List<List<double>>();
            List<double> list = listViewValues.Items.Cast<ListViewItem>()
            .Select(item => Double.Parse(item.Text)).ToList();
            sum(list, Double.Parse(textBoxTargetBalance.Text), new List<double>());
            assignColor();
        }

        private void sum(List<double> list, double target, List<double> partial) {
            double s = 0.0;
            foreach (double n in partial) {
                s += n;
            }

            if (s == target) {
                sums.Add(partial);
            }

            if (s >= target) {
                return;
            }

            for (int i = 0; i < list.Count; i++) {
                List<double> remaining = new List<double>();
                double n = list[i];
                for (int j = i + 1; j < list.Count; j++) {
                    remaining.Add(list[j]);
                }

                List<double> partial_rec = new List<double>(partial);
                partial_rec.Add(n);
                sum(remaining, target, partial_rec);
            }
        }

        private void assignColor() {
            clearColor();
            for (int i = 0; i < this.sums.Count; i++) {
                for (int j = 0; j < this.sums[i].Count; j++) {
                    for (int k = 0; k < listViewValues.Items.Count; k++) {
                        double listVal = Double.Parse(listViewValues.Items[k].Text);
                        if (this.sums[i][j] == listVal) {
                            listViewValues.Items[k].BackColor = Color.LightGreen;
                        }
                    }
                }
            }
        }

        private void clearColor() {
            for (int k = 0; k < listViewValues.Items.Count; k++) {
                listViewValues.Items[k].BackColor = Color.White;
            }
        }

        private void buttonClearList_Click(object sender, EventArgs e) {
            listViewValues.Clear();
        }

        private void AdderInator_MouseDown(object sender, MouseEventArgs e) {
            buttonAdd.Focus();
        }

        private void keyDownHandler(object sender, KeyEventArgs e) {
            if ((textBoxTargetBalance as Control).Focused) {
                if (e.KeyValue == 13) {
                    buttonAdd.Focus();
                }
                return;
            }

            if (e.KeyValue == 48 || e.KeyValue == 96) {
                buttonZero_Click(sender, e);
            } else if (e.KeyValue == 49 || e.KeyValue == 97) {
                buttonOne_Click(sender, e);
            } else if (e.KeyValue == 50 || e.KeyValue == 98) {
                buttonTwo_Click(sender, e);
            } else if (e.KeyValue == 51 || e.KeyValue == 99) {
                buttonThree_Click(sender, e);
            } else if (e.KeyValue == 52 || e.KeyValue == 100) {
                buttonFour_Click(sender, e);
            } else if (e.KeyValue == 53 || e.KeyValue == 101) {
                buttonFive_Click(sender, e);
            } else if (e.KeyValue == 54 || e.KeyValue == 102) {
                buttonSix_Click(sender, e);
            } else if (e.KeyValue == 55 || e.KeyValue == 103) {
                buttonSeven_Click(sender, e);
            } else if (e.KeyValue == 56 || e.KeyValue == 104) {
                buttonEight_Click(sender, e);
            } else if (e.KeyValue == 57 || e.KeyValue == 105) {
                buttonNine_Click(sender, e);
            } else if (e.KeyValue == 13) {
                buttonAdd_Click(sender, e);
            } else if (e.KeyValue == 67) {
                buttonClear_Click(sender, e);
            } else if (e.KeyValue == 8) {
                buttonDelete_Click(sender, e);
            } else if (e.KeyValue == 190 || e.KeyValue == 110) {
                buttonDecimal_Click(sender, e);
            }
        }

        private void textBoxTargetBalance_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1) {
                e.Handled = true;
            }
        }
    }
}
