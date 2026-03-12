using System;

class WeighingMachine
{
    public WeighingMachine(int precision)
    {
        Precision = precision;
    }

    public int Precision { get; }

    private double m_Weight;
    public double Weight { get=>m_Weight; 
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException();
            m_Weight = value;
        }
    }

    public string DisplayWeight => string.Format($"{{0:F{Precision}}} kg", Weight-TareAdjustment);

    public double TareAdjustment {get;set;} = 5.0;
}
