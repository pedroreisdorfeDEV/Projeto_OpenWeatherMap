namespace projetoGloboClima.Shared.OutPut
{
    public class Notification
    {
        // <"NomeErro","Mensagem de erro">
        private readonly Dictionary<string, string> ErrorMessages = new();
        public bool HasError => ErrorMessages.Any();
        public Dictionary<string, string> GetErrorMessages() => ErrorMessages;
        public void AddErrorMessages(Dictionary<string, string> errors)
        {
            foreach (var error in errors)
            {
                AddErrorMessages(error.Key, error.Value);
            }
        }
        public void AddErrorMessages(string key, string message)
        {
            ErrorMessages.TryAdd(key, message);
        }
    }
}
