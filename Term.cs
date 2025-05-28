// term file, which recursively breaks down the differentiand into terms,
// then assumes every term is made from two subsequent terms, and so on and so forth,
// until every term consists of simple differentiable functions. Then it works from the bottom,
// finding derivatives of more complicated terms based on relationships of smaller ones.

using System.ComponentModel.Design;
using System.Numerics;

public class Term {
    public string term;

    public Term numba1;
    public Term numba2;
    public char operato;
    public string derivative;

    public float coefficient;
    public char variable;
    public float exponent;

    public bool singularFunction;
    public static string[,] specialDerivatives = {
        {"cos(x)", "sin(x)", "tan(x)", "sec(x)", "csc(x)", "cot(x)", "ln(x)"},
        {"-sin(x)", "cos(x)", "sec^2(x)", "sec(x) * tan(x)", "-csc(x) * cot(x)", "-csc^2(x)", "1/x"}
    };

    public Term(string t) {
        term = t;
        Differentiate();
    }

    public void Differentiate() {
        SplitTerm();
        switch(operato) {
            case '+':
                derivative = numba1.derivative + "+" + numba2.derivative;
                break;
            case '-':
                derivative = numba1.derivative + "-" + numba2.derivative;
                break;
            case '*':
                derivative = "(" + numba1.derivative + "*" + numba2.term + ")+(" + numba1.term + "*" + numba2.derivative + ")";
                break;
            case '/':
                derivative = "((" + numba2.term + "*" + numba1.derivative + ")-(" + numba1.term + "*" + numba2.derivative + ")/(" + numba2.term + "^2)";
                break;
            case 'f':
                derivative = FindComponents();
                break;
        }
    }
    public void SplitTerm() {
        int parentheseCount = 0;
        for(int i = 0; i < term.Length; i++) {
            if(term[i] == '(') {
                parentheseCount++;
            }
            if(term[i] == ')') {
                parentheseCount--;
            }

            if(parentheseCount == 0) {
                if(term[i] == '*' || term[i] == '/' || term[i] == '+' || term[i] == '-') {
                    numba1 = new Term(term.Substring(0, i));
                    numba2 = new Term(term.Substring(i + 1));
                    operato = term[i];
                }
            }
        }
        if(numba1 == null || numba2 == null) {
            if(term[0] == '(' && term[term.Length - 1] == ')') {
                term = term.Substring(1, term.Length - 2);
                SplitTerm();
                } else {
                    operato = 'f';
                    singularFunction = true;
                }
            }
        }
    public string FindComponents() {
        for(int sp = 0; sp < 2; sp++) {
            if(term.Equals(specialDerivatives[0, sp])) {
                return specialDerivatives[1, sp];
            }
        }
        int i = 0;
        while(i < term.Length && Char.IsDigit(term[i]) == true) {
            i++;
        }
        if(i > 0) 
            coefficient = float.Parse(term.Substring(0, i));
        else 
            coefficient = 1;
        
        int r = i;
        while(i < term.Length && Char.IsLetter(term[i]) == true) {
            i++;
        }
        if(i > r) 
            variable = Convert.ToChar(term.Substring(r, i - r));
        else 
            variable = '?';

        if(variable == '?') {
            return "0";
        } else {
            exponent = 1;
        }
        if (term.IndexOf("^") == -1) {
            return "" + coefficient * exponent;
        }
        else {
            exponent = float.Parse(term.Substring(term.IndexOf("^") + 1));
        }

        if(variable != Derivative.withRespectTo) {
            return "" + (coefficient * exponent) + variable + "^" + (exponent - 1) + "*(d" + variable + "/d" + Derivative.withRespectTo + ")";
        }
        

        return "" + (coefficient * exponent) + variable + "^" + (exponent - 1);
    }
}