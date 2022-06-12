using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CalcuMVVM.ViewModel
{
    public class CalcuVM: VMBase
    {
        #region Propiedades
        int currentState = 1;

        double firstNumber;
        public double FirstNumber
        {
            get { return firstNumber; }
            set
            {
                if (firstNumber != value)
                {
                    firstNumber = value;
                    OnPropertyChange();
                }
            }
        }

        double secondNumber;
        public double SecondNumber
        {
            get { return secondNumber; }
            set
            {
                if (secondNumber != value)
                {
                    secondNumber = value;
                    OnPropertyChange();
                }
            }
        }

        string mathOperator;
        public string MathOperator
        {
            get { return mathOperator; }
            set
            {
                if (mathOperator != value)
                {
                    mathOperator = value;
                    OnPropertyChange();
                }
            }
        }

        string result;
        public string Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    OnPropertyChange();
                }
            }
        }
        #endregion

        #region Comandos
        public ICommand OnSelectNumber { protected set; get; }

        public ICommand OnSelectOperator { protected set; get; }

        public ICommand OnClear { protected set; get; }

        public ICommand OnCalculate { protected set; get; }
        #endregion

        #region Constructor
        public CalcuVM()
        {
            OnSelectNumber = new Command<string>(execute: (string paramerter) =>
            {
                string pressed = paramerter;

                if (this.Result == "0" || currentState < 0)
                {
                    this.Result = "";
                    if (currentState < 0)
                        currentState *= -1;
                }

                this.Result += pressed;

                double number;
                if (double.TryParse(this.Result, out number))
                {
                    this.Result = number.ToString("N0");
                    if (currentState == 1)
                    {
                        this.FirstNumber = number;
                    }
                    else
                    {
                        this.SecondNumber = number;
                    }
                }
            });

            OnSelectOperator = new Command<string>(execute: (string parameter) =>
            {
                currentState = -2;
                string pressed = parameter;
                this.MathOperator = pressed;
            });

            OnClear = new Command(() =>
            {
                this.FirstNumber = 0;
                this.SecondNumber = 0;
                currentState = 1;
                this.Result = "0";

            });

            OnCalculate = new Command(() =>
            {
                if (currentState == 2)
                {
                    var resultOp = SimpleCalculator.Calculate(this.FirstNumber, this.SecondNumber, this.MathOperator);

                    this.Result = resultOp.ToString();
                    this.FirstNumber = resultOp;
                    currentState = -1;
                }
            });
        }
        #endregion
    }
}
