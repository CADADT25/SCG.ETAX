using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.SubDivision
{
    public class TISICitySubDivisionNameModel
    {

		[XmlRoot(ElementName = "documentation")]
		public class Documentation
		{

			[XmlElement(ElementName = "Name")]
			public string? Name { get; set; }

			[XmlAttribute(AttributeName = "lang")]
			public string? Lang { get; set; }

			[XmlText]
			public string? Text { get; set; }
		}

		[XmlRoot(ElementName = "annotation")]
		public class Annotation
		{

			[XmlElement(ElementName = "documentation")]
			public Documentation? Documentation { get; set; }
		}

		[XmlRoot(ElementName = "enumeration")]
		public class Enumeration
		{

			[XmlElement(ElementName = "annotation")]
			public Annotation? Annotation { get; set; }

			[XmlAttribute(AttributeName = "value")]
			public int Value { get; set; }

			[XmlText]
			public string? Text { get; set; }
		}

		[XmlRoot(ElementName = "restriction")]
		public class Restriction
		{

			[XmlElement(ElementName = "enumeration")]
			public List<Enumeration>? Enumeration { get; set; }

			[XmlAttribute(AttributeName = "base")]
			public string? Base { get; set; }

			[XmlText]
			public string? Text { get; set; }
		}

		[XmlRoot(ElementName = "simpleType")]
		public class SimpleType
		{

			[XmlElement(ElementName = "restriction")]
			public Restriction? Restriction { get; set; }

			[XmlAttribute(AttributeName = "name")]
			public string? Name { get; set; }

			[XmlText]
			public string? Text { get; set; }
		}

		[XmlRoot(ElementName = "schema")]
		public class Schema
		{

			[XmlElement(ElementName = "simpleType")]
			public SimpleType? SimpleType { get; set; }

			[XmlAttribute(AttributeName = "xsd")]
			public string? Xsd { get; set; }

			[XmlAttribute(AttributeName = "ids5ISO316612A")]
			public string? Ids5ISO316612A { get; set; }

			[XmlAttribute(AttributeName = "ccts")]
			public string? Ccts { get; set; }

			[XmlAttribute(AttributeName = "targetNamespace")]
			public string? TargetNamespace { get; set; }

			[XmlAttribute(AttributeName = "elementFormDefault")]
			public string? ElementFormDefault { get; set; }

			[XmlAttribute(AttributeName = "attributeFormDefault")]
			public string? AttributeFormDefault { get; set; }

			[XmlAttribute(AttributeName = "version")]
			public DateTime Version { get; set; }

			[XmlAttribute(AttributeName = "thcitysubdivision")]
			public string? Thcitysubdivision { get; set; }

			[XmlText]
			public string? Text { get; set; }
		}


	}
}
