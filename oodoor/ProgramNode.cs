namespace RaylibConsole.oodoor;

public class ProgramNode
{
    public List<ASTNode> Statements { get; }

    public ProgramNode(List<ASTNode> statements)
    {
        Statements = statements;
    }    
}