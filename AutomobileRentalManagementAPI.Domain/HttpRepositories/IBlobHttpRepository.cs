namespace AutomobileRentalManagementAPI.Domain.HttpRepositories
{
    public interface IBlobHttpRepository
    {
        string UploadBase64FileAndReturnPublicUrl(string base64img);
    }
}