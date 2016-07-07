using EPiServer.Forms.EditView;
using EPiServer.Forms.Samples.Implementation.Elements;
using EPiServer.Shell;

namespace EPiServer.Forms.Samples.EditView
{
    [UIDescriptorRegistration]
    public class DateTimeElementBlockDescriptor : FormElementBlockDescriptor<DateTimeElementBlock> { }

    [UIDescriptorRegistration]
    public class RecaptchaElementBlockDescriptor : FormElementBlockDescriptor<RecaptchaElementBlock> { }
}
