using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NUnit.Framework;
using ServiceStack.Common.Tests.Models;

namespace ServiceStack.Text.Tests.JsonTests
{
	[TestFixture]
	public class BasicJsonTests
		: TestBase
	{
        public class ProductPageModel
        {
            static string[] unallowedAdTheme = new[] { "charm", "bd", "Littérature érotique" };

            public int Id { get; set; }

            const int maxBreadcrumbLength = 188;


            public bool AllowStreaming { get; set; }
            public bool HasHtml { get; set; }
            public string CssUrl { get; set; }

            public string HtmlContent { get; set; }
            public string PreviewText { get; set; }

            public string Description { get; set; }

            public string AdImage { get; set; }
            public string AdLink { get; set; }
            public string AdLargeImage { get; set; }
            public string AdLargeLink { get; set; }

            public string ThumbnailUrl { get; set; }
            public string ImageUrl { get; set; }
            public string Title { get; set; }
            public string BreadcrumbsTitle { get; set; }

            public bool ShowTags { get; set; }

            public int DocumentId { get; set; }

            public bool AllowAd { get; set; }

            public bool IsFree { get; set; }
            public bool IsAdultContent { get; set; }
            public bool HasLanguages { get; set; }
            public bool HasLicence { get; set; }
            public int NbPages { get; set; }
            public string Url { get; set; }
            public string EAN13 { get; set; }
            public string Source { get; set; }

            public bool IsConnected { get; set; }
            public bool IsCatalogSubscriber { get; set; }
            public bool AllowAutoActivityFeed { get; set; }
            public bool ShowActivityFeed { get; set; }
            public string FacebookAccessToken { get; set; }
            public bool IsPublic { get; set; }
            public bool ShowLeadSharing { get; set; }

            public int? StreamingVersion { get; set; }


            private IEnumerable<int> meteoJbTeasingThemeId = new[] { 25, 99 };

            public string FacebookAppId { get; set; }

            public int ToasterCount { get; set; }
        }

		public class JsonPrimitives
		{
			public int Int { get; set; }
			public long Long { get; set; }
			public float Float { get; set; }
			public double Double { get; set; }
			public bool Boolean { get; set; }
			public DateTime DateTime { get; set; }
			public string NullString { get; set; }

			public static JsonPrimitives Create(int i)
			{
				return new JsonPrimitives
				{
					Int = i,
					Long = i,
					Float = i,
					Double = i,
					Boolean = i % 2 == 0,
					DateTime = new DateTime(DateTimeExtensions.UnixEpoch + (i * DateTimeExtensions.TicksPerMs), DateTimeKind.Utc),
				};
			}
		}

		[Test]
		public void Can_handle_json_primitives()
		{
			var json = JsonSerializer.SerializeToString(JsonPrimitives.Create(1));
			Log(json);

			Assert.That(json, Is.EqualTo(
				"{\"Int\":1,\"Long\":1,\"Float\":1,\"Double\":1,\"Boolean\":false,\"DateTime\":\"\\/Date(1+0000)\\/\"}"));
		}

		[Test]
		public void Can_parse_json_with_nulls()
		{
			const string json = "{\"Int\":1,\"NullString\":null}";
			var value = JsonSerializer.DeserializeFromString<JsonPrimitives>(json);

			Assert.That(value.Int, Is.EqualTo(1));
			Assert.That(value.NullString, Is.Null);
		}

        [Test]
        public void Can_parse_json_complicated()
        {
            const string json = "{\"Id\":2491780,\"Category\":{\"Id\":8,\"Name\":\"book\",\"Description\":\"Livres\",\"Label\":\"Livres\",\"IsPublic\":true},\"Theme\":{\"Id\":111,\"Name\":\"charm\",\"Description\":\"Sensuelles ou torrides, des lectures libertines pour mettre vos sens en \xc3\xa9veil et affoler votre imagination\xe2\x80\xa6\",\"Label\":\"Charme\",\"Title\":\"Histoires \xc3\xa9rotiques - Charme - YouScribe.com\",\"SortOrder\":15},\"ThemeIds\":[111],\"ThemeName\":\"charm\",\"ProductTitleMaxLength\":176,\"Publisher\":{\"Id\":244951,\"Name\":\"ska-editeur\",\"PublishDate\":\"\\/Date(1409652578000+0000)\\/\",\"Following\":false,\"PhotoUrl\":\"http://img.uscri.be/ath/bc2611aa5b462d62c5f6f9d0de785ec9712a3bcb.jpg\",\"DomainLanguage\":\"fr\",\"ShowFollowingAndMessage\":true,\"ProductId\":2491780,\"IsAuthenticated\":false,\"AllowAd\":true,\"AllowComment\":true},\"AddToCart\":{\"Formats\":[{\"FormatDescription\":\"Pour lire ce <strong>fichier PDF avec DRM</strong>, vous devez installer le logiciel gratuit Adobe Reader\\u00AE. <a href=\\\"http://get.adobe.com/fr/reader/\\\"  target=\\\"_blank\\\">T\\u00E9l\\u00E9charger ce logiciel</a>.<br/> Pour en savoir plus, consultez notre tutoriel <a href=\\\"#\\\">Lire et transf\\u00E9rer des fichiers prot\\u00E9g\\u00E9s par DRM</a>\",\"DocumentId\":0,\"DocumentFormatId\":11366045,\"FormatTypeId\":1,\"IsOriginal\":false,\"Key\":\"e7e910870f503fc8f6d5b5db98cfd3e2722a677e.pdf\",\"Size\":3168226,\"FormatName\":\"PDF\",\"FormatExtension\":\"pdf\",\"FormatMimeType\":\"application/pdf\",\"IsPublic\":true},{\"FormatDescription\":\"L\\u2019ePub est un format particuli\\u00E8rement adapt\\u00E9 \\u00E0 la lecture sur appareils mobiles. Pour lire ce <strong>fichier ePub avec DRM</strong>, vous devez t\\u00E9l\\u00E9charger le logiciel (gratuit) Adobe Digital Edition\\u00AE. <a href=\\\"http://www.adobe.com/ca_fr/products/digitaleditions/\\\">T\\u00E9l\\u00E9charger ce logiciel</a>.\",\"DocumentId\":0,\"DocumentFormatId\":11366038,\"FormatTypeId\":23,\"IsOriginal\":true,\"Key\":\"e7e910870f503fc8f6d5b5db98cfd3e2722a677e.epub\",\"Size\":507616,\"FormatName\":\"EPUB\",\"FormatExtension\":\"epub\",\"FormatMimeType\":\"application/epub+zip\",\"IsPublic\":true}],\"Offers\":[{\"AllowStreaming\":true,\"AllowDownload\":false,\"PriceInEuro\":0,\"DiffPriceInEuro\":1.49,\"HasDrm\":false,\"IsFree\":true,\"ProductId\":2491780,\"Formats\":[{\"FormatDescription\":\"Pour lire ce <strong>fichier PDF avec DRM</strong>, vous devez installer le logiciel gratuit Adobe Reader\\u00AE. <a href=\\\"http://get.adobe.com/fr/reader/\\\"  target=\\\"_blank\\\">T\\u00E9l\\u00E9charger ce logiciel</a>.<br/> Pour en savoir plus, consultez notre tutoriel <a href=\\\"#\\\">Lire et transf\\u00E9rer des fichiers prot\\u00E9g\\u00E9s par DRM</a>\",\"DocumentId\":0,\"DocumentFormatId\":11366045,\"FormatTypeId\":1,\"IsOriginal\":false,\"Key\":\"e7e910870f503fc8f6d5b5db98cfd3e2722a677e.pdf\",\"Size\":3168226,\"FormatName\":\"PDF\",\"FormatExtension\":\"pdf\",\"FormatMimeType\":\"application/pdf\",\"IsPublic\":true},{\"FormatDescription\":\"L\\u2019ePub est un format particuli\\u00E8rement adapt\\u00E9 \\u00E0 la lecture sur appareils mobiles. Pour lire ce <strong>fichier ePub avec DRM</strong>, vous devez t\\u00E9l\\u00E9charger le logiciel (gratuit) Adobe Digital Edition\\u00AE. <a href=\\\"http://www.adobe.com/ca_fr/products/digitaleditions/\\\">T\\u00E9l\\u00E9charger ce logiciel</a>.\",\"DocumentId\":0,\"DocumentFormatId\":11366038,\"FormatTypeId\":23,\"IsOriginal\":true,\"Key\":\"e7e910870f503fc8f6d5b5db98cfd3e2722a677e.epub\",\"Size\":507616,\"FormatName\":\"EPUB\",\"FormatExtension\":\"epub\",\"FormatMimeType\":\"application/epub+zip\",\"IsPublic\":true}],\"AccessLabel\":\"<span>Lecture en ligne</span>\",\"IsSubscription\":true},{\"AllowStreaming\":true,\"AllowDownload\":true,\"PriceInEuro\":1.49,\"DiffPriceInEuro\":0,\"HasDrm\":false,\"IsFree\":false,\"ProductId\":2491780,\"Formats\":[{\"FormatDescription\":\"Pour lire ce <strong>fichier PDF avec DRM</strong>, vous devez installer le logiciel gratuit Adobe Reader\\u00AE. <a href=\\\"http://get.adobe.com/fr/reader/\\\"  target=\\\"_blank\\\">T\\u00E9l\\u00E9charger ce logiciel</a>.<br/> Pour en savoir plus, consultez notre tutoriel <a href=\\\"#\\\">Lire et transf\\u00E9rer des fichiers prot\\u00E9g\\u00E9s par DRM</a>\",\"DocumentId\":0,\"DocumentFormatId\":11366045,\"FormatTypeId\":1,\"IsOriginal\":false,\"Key\":\"e7e910870f503fc8f6d5b5db98cfd3e2722a677e.pdf\",\"Size\":3168226,\"FormatName\":\"PDF\",\"FormatExtension\":\"pdf\",\"FormatMimeType\":\"application/pdf\",\"IsPublic\":true},{\"FormatDescription\":\"L\\u2019ePub est un format particuli\\u00E8rement adapt\\u00E9 \\u00E0 la lecture sur appareils mobiles. Pour lire ce <strong>fichier ePub avec DRM</strong>, vous devez t\\u00E9l\\u00E9charger le logiciel (gratuit) Adobe Digital Edition\\u00AE. <a href=\\\"http://www.adobe.com/ca_fr/products/digitaleditions/\\\">T\\u00E9l\\u00E9charger ce logiciel</a>.\",\"DocumentId\":0,\"DocumentFormatId\":11366038,\"FormatTypeId\":23,\"IsOriginal\":true,\"Key\":\"e7e910870f503fc8f6d5b5db98cfd3e2722a677e.epub\",\"Size\":507616,\"FormatName\":\"EPUB\",\"FormatExtension\":\"epub\",\"FormatMimeType\":\"application/epub+zip\",\"IsPublic\":true}],\"AccessLabel\":\"<span>Lecture en ligne</span><span>  + </span><span>T\xc3\xa9l\xc3\xa9chargement</span>\",\"IsSubscription\":false}],\"Access\":0,\"UserHasOneClick\":false,\"HasMP3Watermark\":false,\"IsFree\":false,\"IsAvailableInSubscription\":true,\"ProductId\":2491780},\"Rate\":{\"ProductId\":2491780,\"IsAuthenticated\":false,\"AvgScore\":0},\"Stats\":{\"Stats\":{\"NbReads\":0,\"NbComments\":0,\"NbDownloads\":0,\"NbVote\":0,\"AvgScore\":0},\"IsFree\":false},\"AllowStreaming\":true,\"HasHtml\":true,\"CssUrl\":\"//dcss.uscri.be/491ec8debc209466617db1bb4914f90547bf847f.css\",\"HtmlContent\":\"<div style=\\\"width: 1020px; height: 1506px;\\\" pg=\\\"0\\\" id=\\\"Page_Id_0\\\" class=\\\"ys_hid_top_container\\\"><div class=\\\"hid_container\\\"><div class=\\\"hid_container2\\\"><div class=\\\"eCC eCCss1\\\" id=\\\"eC0\\\"><p class=\\\"p0\\\"><img id=\\\"eCSt0\\\" class=\\\"eCImg\\\" alt=\\\"cover.jpg\\\" src=\\\"http://dimg.uscri.be/9b5e76f5d83238d4cb6158306e6e0e2d75468824.jpg\\\"></p></div><div class=\\\"eCC eCCss0\\\" id=\\\"eC1\\\"><p class=\\\"p1\\\"><span class=\\\"t2\\\">J\\u00E9r\\u00E9my Bouquin</span></p><p class=\\\"p1\\\"><span class=\\\"t4\\\">\\u00A0</span></p><p class=\\\"p1\\\"><span class=\\\"t5\\\">L\\u2019archange</span></p><p class=\\\"p1\\\"><span class=\\\"t7\\\">nouvelle</span></p><p class=\\\"p1\\\"><span class=\\\"t7\\\">\\u00A0</span></p><p class=\\\"p1\\\"><span class=\\\"t7\\\">\\u00A0</span></p><p class=\\\"p1\\\"><span class=\\\"t7\\\">Collection</span><span class=\\\"t8\\\"></span><span class=\\\"t9\\\">M\\u00E9langes</span></p><p class=\\\"p1\\\"><span class=\\\"t6\\\"><img width=\\\"71\\\" height=\\\"85\\\" class=\\\"eCImg\\\" alt=\\\"img1.jpg\\\" src=\\\"http://dimg.uscri.be/b50e32409842d200edb89effcd561a5614345de8.jpg\\\"></span></p></div><div class=\\\"eCC eCCss0 eC2-cut\\\" id=\\\"eC2-1\\\" style=\\\"margin-top: 0px; padding-top: 0px; margin-bottom: 0px; padding-bottom: 0px;\\\"><p class=\\\"p0\\\" id=\\\"eC2_0\\\"><span class=\\\"t6\\\">\\u00A0</span></p><p id=\\\"eC2_1\\\"><span class=\\\"t10\\\">\\u00A0</span></p><p id=\\\"eC2_2\\\"><span class=\\\"t11\\\">\\u00A0</span></p><p id=\\\"eC2_3\\\">\\u2014\\u00A0Comment dit-on\\u00A0?</p><p id=\\\"eC2_4\\\">Je soupire... comment dit-on\\u00A0? Je m'interroge.</p></div></div></div></div>\",\"Description\":\"\\\"Leur redonner un brin d\\u2019humanit\\u00E9, leur dispenser du plaisir avant le grand saut dans l\\u2019au-del\\u00E0, tel est son sacerdoce.<br />JE M\\u2019APPROCHE. Je me pr\\u00E9sente devant elle. Je la regarde. Ses yeux r\\u00E9pondent.<br />\\u2014 Je m'appelle Gabriel.<br />Je prends sa main droite, frip\\u00E9e, gel\\u00E9e.<br />\\u2014 Vous avez froid ?<br />Sa l\\u00E8vre inf\\u00E9rieure gerc\\u00E9e tressaute. Un effort terrible. Son visage est creus\\u00E9 par la fatigue, pliss\\u00E9 par les souffrances.<br />\\u2014 Vous savez pourquoi je suis l\\u00E0 ?<br />Son index remue, sa main que je serre tremble. \\u00AB Elle vous attend \\u00BB ont glouss\\u00E9 les filles de l'accueil.<br />L\\u2019auteur aborde dans cette fiction un sujet \\u00E9minemment d\\u00E9licat : la sexualit\\u00E9 des personnes \\u00E2g\\u00E9es. Le plaisir des sens ne peut-il enjoliver ces petits instants d\\u2019avant le grand basculement ? Un texte marquant une grande attention \\u00E0 l\\u2019humanit\\u00E9 souffrante autant qu\\u2019une certitude que c\\u2019est \\u00AB ici et maintenant \\u00BB que la vie s\\u2019exprime.  \\\"<br />\",\"ShowDescription\":true,\"ShowMoreDescription\":true,\"ThumbnailUrl\":\"http://img.uscri.be/pth/a7ae7b55f28ef8aaf4c4b04ed8263e6ba4a12c4f.png\",\"ImageUrl\":\"http://img.uscri.be/pth/b27eec822560b7b0594749f700fae9d01a643d88.png\",\"Title\":\"L'Archange\",\"BreadcrumbsTitle\":\"L'Archange\",\"Reader\":{\"ProductId\":2491780,\"DocumentId\":2468814,\"IsEmbed\":false,\"AllowStreaming\":true,\"IsInStreamingError\":false,\"Content\":\"<div style=\\\"width: 1020px; height: 1506px;\\\" pg=\\\"0\\\" id=\\\"Page_Id_0\\\" class=\\\"ys_hid_top_container\\\"><div class=\\\"hid_container\\\"><div class=\\\"hid_container2\\\"><div class=\\\"eCC eCCss1\\\" id=\\\"eC0\\\"><p class=\\\"p0\\\"><img id=\\\"eCSt0\\\" class=\\\"eCImg\\\" alt=\\\"cover.jpg\\\" src=\\\"http://dimg.uscri.be/9b5e76f5d83238d4cb6158306e6e0e2d75468824.jpg\\\"></p></div><div class=\\\"eCC eCCss0\\\" id=\\\"eC1\\\"><p class=\\\"p1\\\"><span class=\\\"t2\\\">J\\u00E9r\\u00E9my Bouquin</span></p><p class=\\\"p1\\\"><span class=\\\"t4\\\">\\u00A0</span></p><p class=\\\"p1\\\"><span class=\\\"t5\\\">L\\u2019archange</span></p><p class=\\\"p1\\\"><span class=\\\"t7\\\">nouvelle</span></p><p class=\\\"p1\\\"><span class=\\\"t7\\\">\\u00A0</span></p><p class=\\\"p1\\\"><span class=\\\"t7\\\">\\u00A0</span></p><p class=\\\"p1\\\"><span class=\\\"t7\\\">Collection</span><span class=\\\"t8\\\"></span><span class=\\\"t9\\\">M\\u00E9langes</span></p><p class=\\\"p1\\\"><span class=\\\"t6\\\"><img width=\\\"71\\\" height=\\\"85\\\" class=\\\"eCImg\\\" alt=\\\"img1.jpg\\\" src=\\\"http://dimg.uscri.be/b50e32409842d200edb89effcd561a5614345de8.jpg\\\"></span></p></div><div class=\\\"eCC eCCss0 eC2-cut\\\" id=\\\"eC2-1\\\" style=\\\"margin-top: 0px; padding-top: 0px; margin-bottom: 0px; padding-bottom: 0px;\\\"><p class=\\\"p0\\\" id=\\\"eC2_0\\\"><span class=\\\"t6\\\">\\u00A0</span></p><p id=\\\"eC2_1\\\"><span class=\\\"t10\\\">\\u00A0</span></p><p id=\\\"eC2_2\\\"><span class=\\\"t11\\\">\\u00A0</span></p><p id=\\\"eC2_3\\\">\\u2014\\u00A0Comment dit-on\\u00A0?</p><p id=\\\"eC2_4\\\">Je soupire... comment dit-on\\u00A0? Je m'interroge.</p></div></div></div></div>\",\"ContentIsHtml\":true,\"ThumbnailUrl\":\"http://img.uscri.be/pth/b27eec822560b7b0594749f700fae9d01a643d88.png\",\"ProductUrl\":\"http://www.win-back3/catalogue/livres/charme/l-archange-2491780\",\"ProductTitle\":\"L'Archange\",\"ShowLink\":false,\"AllowDownload\":true,\"IsOwner\":false},\"People\":[{\"FullName\":\"J\xc3\xa9r\xc3\xa9my Bouquin (Auteur)\"}],\"ShowTags\":true,\"DocumentId\":2468814,\"AllowAd\":false,\"IsFree\":false,\"IsAdultContent\":true,\"HasLanguages\":true,\"HasLicence\":true,\"NbPages\":4,\"Languages\":[{\"Id\":1931,\"Label\":\"Fran\xc3\xa7ais\",\"IsoCodeAlpha2\":\"FR\",\"IsoCodeAlpha3\":\"FRA\"}],\"Tags\":[{\"Id\":2800,\"Value\":\"Ska\"},{\"Id\":10805,\"Value\":\"Nouvelle\"},{\"Id\":10876,\"Value\":\"Sexualit\xc3\xa9\"},{\"Id\":75052,\"Value\":\"Archange\"},{\"Id\":117674,\"Value\":\"Personne\"},{\"Id\":6298799,\"Value\":\"Ag\xc3\xa9\"}],\"Licence\":{\"Description\":\"\",\"Label\":\"Tous droits r\xc3\xa9serv\xc3\xa9s\",\"Name\":\"all_rights\",\"ShowTooltip\":false,\"Position\":1,\"Hidden\":false},\"IsConnected\":false,\"IsCatalogSubscriber\":false,\"AllowAutoActivityFeed\":false,\"ShowActivityFeed\":false,\"FacebookAccessToken\":\"\",\"IsPublic\":true,\"ShowLeadSharing\":false,\"StreamingVersion\":2,\"DisplayMode\":\"Infinite\",\"Folders\":[],\"FacebookAppId\":\"185333694813498\",\"ToasterCount\":0}";

            var value = JsonSerializer.DeserializeFromString<ProductPageModel>(json);

            Assert.That(value.Description, Is.EqualTo("\"Leur redonner un brin d’humanité, leur dispenser du plaisir avant le grand saut dans l’au-delà, tel est son sacerdoce.<br />JE M’APPROCHE. Je me présente devant elle. Je la regarde. Ses yeux répondent.<br />— Je m'appelle Gabriel.<br />Je prends sa main droite, fripée, gelée.<br />— Vous avez froid ?<br />Sa lèvre inférieure gercée tressaute. Un effort terrible. Son visage est creusé par la fatigue, plissé par les souffrances.<br />— Vous savez pourquoi je suis là ?<br />Son index remue, sa main que je serre tremble. « Elle vous attend » ont gloussé les filles de l'accueil.<br />L’auteur aborde dans cette fiction un sujet éminemment délicat : la sexualité des personnes âgées. Le plaisir des sens ne peut-il enjoliver ces petits instants d’avant le grand basculement ? Un texte marquant une grande attention à l’humanité souffrante autant qu’une certitude que c’est « ici et maintenant » que la vie s’exprime.  \"<br />"));
        }

        [Test]
        public void Can_serialize_dictionary_of_int_int()
        {
            var json = JsonSerializer.SerializeToString<IntIntDictionary>(new IntIntDictionary() {Dictionary = {{10,100},{20,200}}});
            const string expected = "{\"Dictionary\":{\"10\":100,\"20\":200}}";
            Assert.That(json,Is.EqualTo(expected));
        }

        private class IntIntDictionary
        {
            public IntIntDictionary()
            {
                Dictionary = new Dictionary<int, int>();
            }
            public IDictionary<int,int> Dictionary { get; set; }
        }

        [Test]
        public void Serialize_skips_null_values_by_default()
        {
            var o = new NullValueTester
            {
                Name = "Brandon",
                Type = "Programmer",
                SampleKey = 12,
                Nothing = (string)null
            };
            
            var s = JsonSerializer.SerializeToString(o);
            Assert.That(s, Is.EqualTo("{\"Name\":\"Brandon\",\"Type\":\"Programmer\",\"SampleKey\":12}"));
        }

        [Test]
        public void Serialize_can_include_null_values()
        {
            var o = new NullValueTester
            {
                Name = "Brandon",
                Type = "Programmer",
                SampleKey = 12,
                Nothing = null
            };
            
            JsConfig.IncludeNullValues = true;
            var s = JsonSerializer.SerializeToString(o);
            JsConfig.IncludeNullValues = false;
            Assert.That(s, Is.EqualTo("{\"Name\":\"Brandon\",\"Type\":\"Programmer\",\"SampleKey\":12,\"Nothing\":null}"));
        }

        [Test]
        public void Deserialize_sets_null_values()
        {
            var s = "{\"Name\":\"Brandon\",\"Type\":\"Programmer\",\"SampleKey\":12,\"Nothing\":null}";
            var o = JsonSerializer.DeserializeFromString<NullValueTester>(s);
            Assert.That(o.Name, Is.EqualTo("Brandon"));
            Assert.That(o.Type, Is.EqualTo("Programmer"));
            Assert.That(o.SampleKey, Is.EqualTo(12));
            Assert.That(o.Nothing, Is.Null);
        }

        [Test]
        public void Deserialize_ignores_omitted_values()
        {
            var s = "{\"Type\":\"Programmer\",\"SampleKey\":2}";
            var o = JsonSerializer.DeserializeFromString<NullValueTester>(s);
            Assert.That(o.Name, Is.EqualTo("Miguel"));
            Assert.That(o.Type, Is.EqualTo("Programmer"));
            Assert.That(o.SampleKey, Is.EqualTo(2));
            Assert.That(o.Nothing, Is.EqualTo("zilch"));
        }

        private class NullValueTester
        {
            public string Name
            {
                get;
                set;
            }

            public string Type
            {
                get;
                set;
            }

            public int SampleKey
            {
                get;
                set;
            }

            public string Nothing
            {
                get;
                set;
            }

            public NullValueTester()
            {
                Name = "Miguel";
                Type = "User";
                SampleKey = 1;
                Nothing = "zilch";
            }
        }
		
		[DataContract]
		class Person
		{
			[DataMember(Name = "MyID")]
			public int Id { get; set; }
			[DataMember]
			public string Name { get; set; }
		}

		[Test]
		public void Can_override_name()
		{
			var person = new Person {
				Id = 123,
				Name = "Abc"
			};

			Assert.That(TypeSerializer.SerializeToString(person), Is.EqualTo("{MyID:123,Name:Abc}"));
			Assert.That(JsonSerializer.SerializeToString(person), Is.EqualTo("{\"MyID\":123,\"Name\":\"Abc\"}"));
		}
	}
}