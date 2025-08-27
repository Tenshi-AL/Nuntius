using Microsoft.AspNetCore.Components.Forms;

namespace UI.Extensions;

public static class BrowserFileExtensions
{
    public static async Task<byte[]> ToByteArray(this IBrowserFile browserFile)
    {
        using var memoryStream = new MemoryStream();
        await browserFile.OpenReadStream().CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}