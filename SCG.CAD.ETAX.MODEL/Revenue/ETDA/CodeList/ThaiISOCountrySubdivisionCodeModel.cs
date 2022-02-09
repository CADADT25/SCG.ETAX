using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.Provice
{
    public class ThaiISOCountrySubdivisionCodeModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public class ETDAProvice
        {
            public string? ProviceCode { get; set; }
            public string? ProviceName { get; set; }
        }

        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }

        public class XsdElement
        {
            [JsonProperty("@name")]
            public string Name { get; set; }

            [JsonProperty("@type")]
            public string Type { get; set; }
        }

        public class XsdDocumentation
        {
            [JsonProperty("@xml:lang")]
            public string? XmlLang { get; set; }

            [JsonProperty("ccts:Name")]
            public string? CctsName { get; set; }
        }

        public class XsdAnnotation
        {
            [JsonProperty("xsd:documentation")]
            public XsdDocumentation XsdDocumentation { get; set; }
        }

        public class XsdEnumeration
        {
            [JsonProperty("@value")]
            public string? Value { get; set; }

            [JsonProperty("xsd:annotation")]
            public XsdAnnotation? XsdAnnotation { get; set; }
        }

        public class XsdRestriction
        {
            [JsonProperty("@base")]
            public string Base { get; set; }

            [JsonProperty("xsd:enumeration")]
            public List<XsdEnumeration> XsdEnumeration { get; set; }
        }

        public class XsdSimpleType
        {
            [JsonProperty("@name")]
            public string Name { get; set; }

            [JsonProperty("xsd:restriction")]
            public XsdRestriction XsdRestriction { get; set; }
        }

        public class XsdSchema
        {
            [JsonProperty("@xmlns:xsd")]
            public string XmlnsXsd { get; set; }

            [JsonProperty("@xmlns:ids5ISO316612A")]
            public string XmlnsIds5ISO316612A { get; set; }

            [JsonProperty("@xmlns:ccts")]
            public string XmlnsCcts { get; set; }

            [JsonProperty("@targetNamespace")]
            public string TargetNamespace { get; set; }

            [JsonProperty("@elementFormDefault")]
            public string ElementFormDefault { get; set; }

            [JsonProperty("@attributeFormDefault")]
            public string AttributeFormDefault { get; set; }

            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("#comment")]
            public List<object> Comment { get; set; }

            [JsonProperty("xsd:element")]
            public XsdElement XsdElement { get; set; }

            [JsonProperty("xsd:simpleType")]
            public XsdSimpleType XsdSimpleType { get; set; }
        }

        public class Root
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }

            [JsonProperty("#comment")]
            public List<object> Comment { get; set; }

            [JsonProperty("xsd:schema")]
            public XsdSchema XsdSchema { get; set; }
        }


    }
}
