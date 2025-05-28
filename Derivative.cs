// derivate class, which is the main script for the differentiand. Seperates the original
// differentiand, starts the term mitosis, then returns the final result to the console.

public class Derivative {
    public Term differentiand;
    public List<Term> terms;
    public static char? withRespectTo;

    public Derivative(string d, char w) {
        withRespectTo = w;
        differentiand = new Term(String.Concat(d.Where(c => !Char.IsWhiteSpace(c))));
    }

    public string BasicTerm(Term term) {
        return "" + term.coefficient / (term.exponent + 1) + term.variable + "^" + (term.exponent + 1);
    }

    public void CreateTerms() {
        string temp = "differentiand";
        while(temp.IndexOf(" ") == -1) {
            temp = temp.Substring(0, temp.IndexOf(" ")) + temp.Substring(temp.IndexOf(" ") + 1);
        }
        string[] subs = temp.Split("+");
        for(int i = 0; i < subs.Length; i++) {
            terms[i] = new Term(subs[i]);
        }
    }

    public override string ToString() {
        string derivative = differentiand.derivative;
        int i = 0;
        while(i < derivative.Length) {
            if(derivative[i] == '*' || derivative[i] == '/' || derivative[i] == '+' || derivative[i] == '-') {
                derivative = derivative.Substring(0, i) + " " + derivative[i] + " " + derivative.Substring(i + 1);
                i++;
            }
            i++;
        }
        return "" + derivative;
    }
}