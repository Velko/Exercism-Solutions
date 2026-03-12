using System;

public struct ComplexNumber
{
    private readonly double _real;
    private readonly double _imaginary;

    public ComplexNumber(double real, double imaginary)
    {
        _real = real;
        _imaginary = imaginary;
    }

    public static implicit operator ComplexNumber(double real)  
        => new ComplexNumber(real, 0);

    public double Real()
        => _real;

    public double Imaginary()
        => _imaginary;

    public ComplexNumber Mul(ComplexNumber other)
        => new ComplexNumber(
                _real * other._real - _imaginary * other._imaginary,
                _imaginary * other._real + _real * other._imaginary);
        
    public ComplexNumber Add(ComplexNumber other)
        => new ComplexNumber(_real + other._real, _imaginary + other._imaginary);

    public ComplexNumber Sub(ComplexNumber other)
        => new ComplexNumber(_real - other._real, _imaginary - other._imaginary);

    public ComplexNumber Div(ComplexNumber other)
    {
        double otherAbsSq = other._real * other._real
            + other._imaginary * other._imaginary;
        
        return new ComplexNumber(
            (_real * other._real + _imaginary * other._imaginary) / otherAbsSq,
            (_imaginary * other._real - _real * other._imaginary) / otherAbsSq);
    }

    public double Abs()
        => Math.Sqrt(_real * _real + _imaginary * _imaginary);

    public ComplexNumber Conjugate()
        => new ComplexNumber(_real, -_imaginary);
    
    public ComplexNumber Exp()
    {
        double realExp = Math.Exp(_real);

        return new ComplexNumber(
            Math.Cos(_imaginary) * realExp,
            Math.Sin(_imaginary) * realExp
        );
    }
}