PGDMP     *    /                x            GTSport_Test    9.4.5    9.6.15     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false            �           1262    16393    GTSport_Test    DATABASE     �   CREATE DATABASE "GTSport_Test" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_United States.1252' LC_CTYPE = 'English_United States.1252';
    DROP DATABASE "GTSport_Test";
             postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
             postgres    false            �           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                  postgres    false    6            �           0    0    SCHEMA public    ACL     �   REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;
                  postgres    false    6                        3079    11855    plpgsql 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;
    DROP EXTENSION plpgsql;
                  false            �           0    0    EXTENSION plpgsql    COMMENT     @   COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';
                       false    1            �            1259    16593    cars    TABLE     -  CREATE TABLE public.cars (
    carkey character varying(255) NOT NULL,
    caracceleration double precision,
    caraspiration character varying(255),
    carbraking double precision,
    carcategory character varying(255),
    carcornering double precision,
    cardisplacementcc character varying(255),
    cardrivetrain character varying(255),
    carheight double precision,
    carlength double precision,
    carmankey character varying(255),
    carmaxpower integer,
    carmaxspeed double precision,
    carname character varying(255),
    carpowerrpm character varying(255),
    carprice double precision,
    carstability double precision,
    cartorqueftlb double precision,
    cartorquerpm character varying(255),
    carweight double precision,
    carwidth double precision,
    caryear integer
);
    DROP TABLE public.cars;
       public         GTSport    false    6            �            1259    16601 	   countries    TABLE     �   CREATE TABLE public.countries (
    coukey character varying(255) NOT NULL,
    coudescription character varying(255),
    couregkey character varying(255)
);
    DROP TABLE public.countries;
       public         GTSport    false    6            �            1259    16609    keysequence    TABLE     m   CREATE TABLE public.keysequence (
    tablename character varying(255) NOT NULL,
    lastkeyvalue integer
);
    DROP TABLE public.keysequence;
       public         GTSport    false    6            �            1259    16614    manufacturers    TABLE     �   CREATE TABLE public.manufacturers (
    mankey character varying(255) NOT NULL,
    mancoukey character varying(255),
    manname character varying(255)
);
 !   DROP TABLE public.manufacturers;
       public         GTSport    false    6            �            1259    16622 	   ownercars    TABLE     s  CREATE TABLE public.ownercars (
    owckey character varying(255) NOT NULL,
    owcdateaquired date,
    owccolour character varying(255),
    owccarid character varying(255),
    owccarkey character varying(255),
    owcownkey character varying(255),
    owcpowerpoints integer,
    owcmaxpower integer,
    owcpowerlevel integer,
    owcweightreductionlevel integer
);
    DROP TABLE public.ownercars;
       public         GTSport    false    6            �            1259    16630    owners    TABLE     �   CREATE TABLE public.owners (
    ownkey character varying(255) NOT NULL,
    owndefault boolean,
    ownname character varying(255)
);
    DROP TABLE public.owners;
       public         GTSport    false    6            �            1259    16638    regions    TABLE     w   CREATE TABLE public.regions (
    regkey character varying(255) NOT NULL,
    regdescription character varying(255)
);
    DROP TABLE public.regions;
       public         GTSport    false    6            v           2606    16600    cars cars_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.cars
    ADD CONSTRAINT cars_pkey PRIMARY KEY (carkey);
 8   ALTER TABLE ONLY public.cars DROP CONSTRAINT cars_pkey;
       public         GTSport    false    173    173            x           2606    16608    countries countries_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.countries
    ADD CONSTRAINT countries_pkey PRIMARY KEY (coukey);
 B   ALTER TABLE ONLY public.countries DROP CONSTRAINT countries_pkey;
       public         GTSport    false    174    174            z           2606    16613    keysequence keysequence_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public.keysequence
    ADD CONSTRAINT keysequence_pkey PRIMARY KEY (tablename);
 F   ALTER TABLE ONLY public.keysequence DROP CONSTRAINT keysequence_pkey;
       public         GTSport    false    175    175            |           2606    16621     manufacturers manufacturers_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public.manufacturers
    ADD CONSTRAINT manufacturers_pkey PRIMARY KEY (mankey);
 J   ALTER TABLE ONLY public.manufacturers DROP CONSTRAINT manufacturers_pkey;
       public         GTSport    false    176    176            ~           2606    16629    ownercars ownercars_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.ownercars
    ADD CONSTRAINT ownercars_pkey PRIMARY KEY (owckey);
 B   ALTER TABLE ONLY public.ownercars DROP CONSTRAINT ownercars_pkey;
       public         GTSport    false    177    177            �           2606    16637    owners owners_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.owners
    ADD CONSTRAINT owners_pkey PRIMARY KEY (ownkey);
 <   ALTER TABLE ONLY public.owners DROP CONSTRAINT owners_pkey;
       public         GTSport    false    178    178            �           2606    16645    regions regions_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.regions
    ADD CONSTRAINT regions_pkey PRIMARY KEY (regkey);
 >   ALTER TABLE ONLY public.regions DROP CONSTRAINT regions_pkey;
       public         GTSport    false    179    179            �           2606    16647 #   owners uk_ppqoyehkhydsuncawft1s1cex 
   CONSTRAINT     a   ALTER TABLE ONLY public.owners
    ADD CONSTRAINT uk_ppqoyehkhydsuncawft1s1cex UNIQUE (ownname);
 M   ALTER TABLE ONLY public.owners DROP CONSTRAINT uk_ppqoyehkhydsuncawft1s1cex;
       public         GTSport    false    178    178            �           2606    16658 )   manufacturers fk19bh6b5ofioiaonj69x3mstny    FK CONSTRAINT     �   ALTER TABLE ONLY public.manufacturers
    ADD CONSTRAINT fk19bh6b5ofioiaonj69x3mstny FOREIGN KEY (mancoukey) REFERENCES public.countries(coukey);
 S   ALTER TABLE ONLY public.manufacturers DROP CONSTRAINT fk19bh6b5ofioiaonj69x3mstny;
       public       GTSport    false    176    174    1912            �           2606    16653 %   countries fkgeg4r6fdyrhmjd12621yq8blf    FK CONSTRAINT     �   ALTER TABLE ONLY public.countries
    ADD CONSTRAINT fkgeg4r6fdyrhmjd12621yq8blf FOREIGN KEY (couregkey) REFERENCES public.regions(regkey);
 O   ALTER TABLE ONLY public.countries DROP CONSTRAINT fkgeg4r6fdyrhmjd12621yq8blf;
       public       GTSport    false    174    1924    179            �           2606    16663 %   ownercars fkro4plxjsdu3ea856th8glq402    FK CONSTRAINT     �   ALTER TABLE ONLY public.ownercars
    ADD CONSTRAINT fkro4plxjsdu3ea856th8glq402 FOREIGN KEY (owccarkey) REFERENCES public.cars(carkey);
 O   ALTER TABLE ONLY public.ownercars DROP CONSTRAINT fkro4plxjsdu3ea856th8glq402;
       public       GTSport    false    177    1910    173            �           2606    16668 %   ownercars fks382ffnvb8e2m197689g3rruq    FK CONSTRAINT     �   ALTER TABLE ONLY public.ownercars
    ADD CONSTRAINT fks382ffnvb8e2m197689g3rruq FOREIGN KEY (owcownkey) REFERENCES public.owners(ownkey);
 O   ALTER TABLE ONLY public.ownercars DROP CONSTRAINT fks382ffnvb8e2m197689g3rruq;
       public       GTSport    false    1920    177    178            �           2606    16648     cars fkt768iccun83oje9hltn3needs    FK CONSTRAINT     �   ALTER TABLE ONLY public.cars
    ADD CONSTRAINT fkt768iccun83oje9hltn3needs FOREIGN KEY (carmankey) REFERENCES public.manufacturers(mankey);
 J   ALTER TABLE ONLY public.cars DROP CONSTRAINT fkt768iccun83oje9hltn3needs;
       public       GTSport    false    1916    173    176           