-- Table: public.todo

-- DROP TABLE public.todo;

CREATE TABLE public.todo
(
  "Id" uuid NOT NULL,
  "Title" text,
  "Description" text,
  "CompletionDate" date,
  "IsCompleted" boolean,
  CONSTRAINT pk_todo PRIMARY KEY ("Id")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.todo
  OWNER TO yotodo;
