using System.Text;

namespace PostMan.Net.Model.net.jsTest;

public class PmTestResult {

    private string _name;

    public string Name => _name;
    
    private PmStatus _status;

    public PmStatus Status => _status;

    private IList<string> _messages;

    public IList<string> Messages => _messages;

    public PmTestResult(string name) {
        _name = name ;
        _messages = new List<string>();
        _status = PmStatus.None; 
	
    }

    internal void SetStatus(PmStatus status) {
        if (_status != PmStatus.Failure)
        {
            _status = status;
        }
    }

    internal void AddMessage(string message) {
        _messages.Add(message);	
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"FAILED - test {_name}");
        foreach (var message in _messages)
        {
            builder.AppendLine($"\t- {message}");
        }

        return builder.ToString();
    } 
    
}