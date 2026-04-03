
ALTER TABLE "CartProducts" RENAME COLUMN "CartsId" TO "CartId";
ALTER TABLE "CartProducts" RENAME COLUMN "ProductsId" TO "ProductId";
ALTER TABLE "CartProducts" ADD COLUMN "Id" uuid NOT NULL DEFAULT gen_random_uuid();
ALTER TABLE "CartProducts" ADD COLUMN "Quantity" integer NOT NULL DEFAULT 1;
ALTER TABLE "CartProducts" ADD COLUMN "UnitPrice" numeric NOT NULL DEFAULT 0;
ALTER TABLE "CartProducts" ADD COLUMN "TotalPrice" numeric NOT NULL DEFAULT 0;
ALTER TABLE "CartProducts" ADD COLUMN "Discount" numeric NULL;
ALTER TABLE "CartProducts" ADD COLUMN "ProductName" text NOT NULL DEFAULT '';
ALTER TABLE "CartProducts" ADD COLUMN "CreatedAt" timestamp with time zone NOT NULL DEFAULT now();
