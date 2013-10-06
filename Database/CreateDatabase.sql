DROP TABLE IF EXISTS "TextContents";
DROP TABLE IF EXISTS "ContentElements";
DROP TABLE IF EXISTS "Languages";

CREATE TABLE "Languages"
(
	"LanguageId" varchar(10) NOT NULL,
	"Name" varchar(255) NOT NULL,

	CONSTRAINT "PK_Languages"
		PRIMARY KEY ("LanguageId")
);

CREATE TABLE "ContentElements"
(
	"ContentElementId" serial NOT NULL,
	"ContentType" integer NOT NULL,
	"DefaultLanguage" varchar(10) NOT NULL,

	CONSTRAINT "PK_ContentElements"
		PRIMARY KEY ("ContentElementId"),
	CONSTRAINT "FK_ContentElements_Languages"
		FOREIGN KEY ("DefaultLanguage")
		REFERENCES "Languages"("LanguageId")
);

CREATE TABLE "TextContents"
(
	"ContentValueId" serial NOT NULL,
	"ContentElementId" integer NOT NULL,
	"ContentStatus" integer NOT NULL,
	"Language" varchar(10) NOT NULL,
	"Value" text,

	CONSTRAINT "PK_TextContents"
		PRIMARY KEY ("ContentValueId"),
	CONSTRAINT "FK_TextContents_ContentElements"
		FOREIGN KEY ("ContentElementId")
		REFERENCES "ContentElements"("ContentElementId"),
	CONSTRAINT "FK_TextContents_Languages"
		FOREIGN KEY ("Language")
		REFERENCES "Languages"("LanguageId")
);