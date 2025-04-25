INSERT INTO "DeliveryPerson" (
    "Id", "NavigationId", "Identifier", "Name", "Cnpj", "BirthDate",
    "LicenseNumber", "LicenseType", "LicenseImageUrl"
) VALUES (
    2, 'a064addc-f22e-4d22-be71-39aaf8dd956d'::uuid, 'd3f29e65-8f24-4b99-9332-58dbfd9e4a55'::uuid,
    'João da Silva', '54085160000100', '1990-06-14 21:00:00-03',
    '12345678999', 'AB', 'https://i.pravatar.cc/150?img=33'
);

-- Registro 2
INSERT INTO "DeliveryPerson" (
    "Id", "NavigationId", "Identifier", "Name", "Cnpj", "BirthDate",
    "LicenseNumber", "LicenseType", "LicenseImageUrl"
) VALUES (
    3, '6b068a4b-a581-4fc7-98bb-2bc5b6e74ea1'::uuid, 'd3f29e65-8f24-4b99-9332-58dbfd9e4a55'::uuid,
    'João da Silva', '58912817000116', '1990-06-14 21:00:00-03',
    '12345678902', 'A', 'https://i.pravatar.cc/150?img=33'
);

-- Registro 3
INSERT INTO "DeliveryPerson" (
    "Id", "NavigationId", "Identifier", "Name", "Cnpj", "BirthDate",
    "LicenseNumber", "LicenseType", "LicenseImageUrl"
) VALUES (
    4, '495628ae-d8b8-4430-b07e-436bf22bea3d'::uuid, '7e01fdd0-bc61-4564-9c11-9516d4459ba6'::uuid,
    'Maria Oliveira', '12796799000153', '1985-04-09 21:00:00-03',
    '98765432101', 'B', 'https://i.pravatar.cc/150?img=33'
);

-- Registro 4
INSERT INTO "DeliveryPerson" (
    "Id", "NavigationId", "Identifier", "Name", "Cnpj", "BirthDate",
    "LicenseNumber", "LicenseType", "LicenseImageUrl"
) VALUES (
    5, 'cdb8793b-1158-4845-9e61-d1ea77e6b7a4'::uuid, '1fc18bb1-91d6-46b8-b103-22f00c6cf5b0'::uuid,
    'Carlos Souza', '75134309000173', '1992-11-29 22:00:00-03',
    '11122233344', 'AB', 'https://i.pravatar.cc/150?img=33'
);

-- Registro 5
INSERT INTO "DeliveryPerson" (
    "Id", "NavigationId", "Identifier", "Name", "Cnpj", "BirthDate",
    "LicenseNumber", "LicenseType", "LicenseImageUrl"
) VALUES (
    6, '6d24e695-c692-4670-928a-4e059e83dbda'::uuid, 'd3f29e65-8f24-4b99-9332-58dbfd9e4a55'::uuid,
    'João da Silva', '1122333000181', '1990-06-14 21:00:00-03',
    '12345678901', 'AB', 'https://i.pravatar.cc/150?img=33'
);

-- Registro 1
INSERT INTO "Motorcycle" (
    "Id", "NavigationId", "Identifier", "Year", "Model", "LicensePlate"
) VALUES (
    1, '68ab0e5a-1449-49dc-8bd2-6872c1d69b60'::uuid,
    'honda_cg160', 2019, 'Honda CG 160 Start', 'FTR-4587'
);

-- Registro 2
INSERT INTO "Motorcycle" (
    "Id", "NavigationId", "Identifier", "Year", "Model", "LicensePlate"
) VALUES (
    2, '3c8ac008-5a81-48fd-a103-04cc2d0e0eb6'::uuid,
    'yamaha_fazer250', 2024, 'Yamaha Fazer 250 AE', 'GHB-9241'
);

-- Registro 3
INSERT INTO "Motorcycle" (
    "Id", "NavigationId", "Identifier", "Year", "Model", "LicensePlate"
) VALUES (
    3, '6961d233-f4cf-4c4f-9b40-ecd3598727eb'::uuid,
    'suzuki_gsx150', 2022, 'Suzuki GSX-S150', 'JKL-2210'
);

-- Registro 4
INSERT INTO "Motorcycle" (
    "Id", "NavigationId", "Identifier", "Year", "Model", "LicensePlate"
) VALUES (
    4, 'c3699ec4-b6c7-4357-82bd-ee6588306ca7'::uuid,
    'kawasaki_z400', 2021, 'Kawasaki Z400', 'MQX-7878'
);

-- Registro 5
INSERT INTO "Motorcycle" (
    "Id", "NavigationId", "Identifier", "Year", "Model", "LicensePlate"
) VALUES (
    5, '715e3348-d423-48e4-8f7f-46c8d2444ea5'::uuid,
    'honda_biz110', 2023, 'Honda Biz 110i', 'NVA-3045'
);
