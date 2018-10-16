using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lob.Net.Models
{
    public class LobEvent : LobEvent<JObject>
    {
        internal LobEvent()
        {
        }

        internal JsonSerializerSettings SerializerSettings
        {
            get;
            set;
        }

        public LobEvent<PostcardResponse> ToPostcard()
        {
            return To<PostcardResponse>();
        }

        public LobEvent<LetterResponse> ToLetter()
        {
            return To<LetterResponse>();
        }

        public LobEvent<CheckResponse> ToCheck()
        {
            return To<CheckResponse>();
        }

        public LobEvent<AddressResponse> ToAddress()
        {
            return To<AddressResponse>();
        }

        public LobEvent<BankAccountResponse> ToBankAccount()
        {
            return To<BankAccountResponse>();
        }

        private LobEvent<T> To<T>()
        {
            var serializer = JsonSerializer.Create(SerializerSettings);
            return new LobEvent<T>
            {
                Id = Id,
                ReferenceId = ReferenceId,
                EventType = EventType,
                DateCreated = DateCreated,
                Object = Object,
                Body = Body.ToObject<T>(serializer)
            };
        }
    }
}
