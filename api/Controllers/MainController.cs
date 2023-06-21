using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
 
public class MainController : ControllerBase
{
    protected object MapEntity(object entity)
    {
        var entityType = entity.GetType();

        var entityProperties = entityType.GetProperties();

        var response = new Dictionary<string, object>();

        foreach (var property in entityProperties)
        {
            var value = property.GetValue(entity);

            if (value != null) { response[property.Name] = value; }
        }

        return response;
    }
}