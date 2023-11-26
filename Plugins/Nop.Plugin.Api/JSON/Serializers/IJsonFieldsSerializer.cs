namespace Nop.Plugin.Api.JSON.Serializers
{
    using Nop.Plugin.Api.DTOs;
    using Nop.Plugin.Api.Models;
    using System.Collections.Generic;

    public interface IJsonFieldsSerializer
    {
        string Serialize(ISerializableObject objectToSerialize, string fields);

        string Serialize(object objectToSerialize, IList<string> jsonFields = null);
    }
}
