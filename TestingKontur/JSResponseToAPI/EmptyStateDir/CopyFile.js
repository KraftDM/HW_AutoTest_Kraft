const myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIxMzk3NzgwZC03ZDdlLTRjN2UtYWE4NC1iNTgyM2ViNzY0NWYiLCJzaWQiOiI0YTNkNzliZC03MzIwLTQzMGEtOWIwMC04NmNmMGEzNDAwNGYiLCJzdWIiOiJsc3Yua3JhZnRAbWFpbC5ydSIsIkZlYXR1cmUiOlsiUGF5cm9sbHMiLCJUaW1lTGluZSIsIlN1cnZleXMiLCJEb2N1bWVudHMiLCJOZXdzRWRpdG9yIiwiV2lkZ2V0cyIsIkJpcnRoZGF5cyIsIlNheVRoYW5rcyIsIkV4dGVybmFsU2VydmljZXMiLCJGaWxlcyIsIkNoYXRzIiwiR2VvbG9jYXRpb24iLCJFbXBsb3llZXNSZXBvcnQiLCJFdmVudENhdGVnb3JpZXMiLCJBY2hpZXZlbWVudHMiXSwiUm9sZSI6IkV2ZW50c0NyZWF0b3IiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJFdmVudHNDcmVhdG9yIiwiZXhwIjoxNzE0MjE1MTk1LCJpc3MiOiJHQ2xvdWQuU3RhZmYiLCJhdWQiOiJHQ2xvdWQuU3RhZmYifQ.UDe0wRKSbX7X4nPnmyB8ZHkK6bjiXBCiVTet4RVV_yM");

const postRequset = {
    method: "POST",
    headers: myHeaders,
    redirect: "follow"
};

fetch("https://staff-testing.testkontur.ru/api/v1/cfs/dir/e0e8b17f-81ae-4392-8b1f-61d6d2cb8319/files?copy=ade61c11-4577-4533-9b2d-b76a520675c9", postRequset)
    .then((response) => response.text())
    .then((result) => console.log(result))
    .catch((error) => console.error(error));