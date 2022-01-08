using System.Collections.Generic;
using System.Linq;
using Compiler.LexicalAnalysis;

namespace Compiler.SyntacticAnalysis;

internal class LexemeSet: HashSet<LexemeType>
{
    public static readonly LexemeSet Empty =  LexemeSet.From();
    private LexemeSet(IEnumerable<LexemeType> values): base(values)
    {
            
    }
        
    public static LexemeSet From(params LexemeType[] args)
    {
        return new LexemeSet(args);
    }

     
    public static LexemeSet operator+ (LexemeSet left, LexemeSet right) {
        LexemeSet res = new LexemeSet(left);
        foreach (var lexemeType in right)
        {
            res.Add(lexemeType);
        }
        return res;
    }
        
    public static LexemeSet operator+ (LexemeSet left, LexemeType right) {
        LexemeSet res = new LexemeSet(left);
        res.Add(right);
        return res;
    }
        
    public static LexemeSet operator- (LexemeSet left, LexemeSet right) {
        LexemeSet res = new LexemeSet(left);
        foreach (var lexemeType in right)
        {
            res.Remove(lexemeType);
        }
        return res;
    }
        
    public static LexemeSet operator- (LexemeSet left, LexemeType right) {
        LexemeSet res = new LexemeSet(left);
        res.Remove(right);
        return res;
    }

    /// <summary>
    /// Is this an empty set
    /// </summary>
    public bool IsEmpty => !this.Any();

    public override string ToString()
    {
        var items = this.Select(x => x.ToString()).OrderBy(s => s).ToArray();
        return string.Join(",", items);
    }

}