public class Term {
    public string term;

    public Term numba1;
    public Term numba2;
    public char operator;
    public string derivative;

    public float coefficient;
    public char variable;
    public float exponent;

    public bool singularFunction;
    public int specialDerivativeID;

    public Term(string t) {
        term = t;
        Differentiate();
    }

    public void Differentiate() {
        SplitTerm();
        switch(operator) {
            case "+":
                derivative = numba1.derivative + "+" + numba2.derivative;
                break;
            case "-":
                derivative = numba1.derivative + "-" + numba2.derivative;
                break;
            case "*":
                derivative = "("numba1.derivate + "*" + numba2 + ")+(" + numba1 + "*" + numba2.derivative + ")";
                break;
            case "/":
                break;
            case "f":
                derivative = FindComponents();
                break;
        }
    }
    public void SplitTerm() {
        int parentheseCount = 0;
        for(int i = 0; i < term.length; i++) {
            if(term[i] == "(") {
                parentheseCount++;
            }
            if(term[i] == ")") {
                parentheseCount--;
            }

            if(parentheseCount == 0) {
                if(term[i] == "*" || term[i] == "/" || term[i] == "+" || term[i] == "-") {
                    numba1 = new Term(term.Substring(0, i));
                    numba2 = new Term(term.Substring(i + 1));
                    operator = term[i];
                }
            }
        }
        if(numba1 == null || numba2 == null) {
            if(term[0] == "(" && term[term.length - 1] == ")") {
                term = term.Substring(1, term.length - 2);
                SplitTerm();
            } else {
                operator = "f";
                singularFunction = true;
            }
        }
    }
    public string FindComponents() {
        for(int i = 0; i < specialDerivatives[0].length; i++) {
            if(term.equals(specialDerivatives[0, i])) {
                return specialDerivatives[1, i];
            }
        }
        int i = 0;
        while(Char.isDigit(term[i]) == true) {
            i++;
        }
        coefficient = term.Substring(0, i);
        
        int r = i;
        while(Char.isLetter(term[i]) == true) {
            i++;
        }
        variable = term.Substring(r, i - r);


        return "" + (coefficient * exponent) + variable + "^" + (exponent - 1);
    }
}