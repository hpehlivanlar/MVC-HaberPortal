
function KategoriEkle()
{
    Kategori = new Object();
    Kategori.KategoriAdi = $("#kategoriAdi").val();
    Kategori.Url = $("#kategoriUrl").val();
    Kategori.AktifMi = $("#kategoriAktif").is(":checked");
    Kategori.ParentID = $("#ParentID").val();


    $.ajax({
        url: "/Kategori/Ekle",
        data: Kategori,
        type: "POST",
        dataType: 'json',
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });

            }
            else {
                bootbox.alert(response.Message, function () {
                    //işlem sonucunda yapılcak işlemler burada yazılır
                });
            }
        }
    })

}

$(document).on("click", "#KategoriDelete", function () {
    var gelenID = $(this).attr("data-id");
    var silTR = $(this).closest("tr");
    $.ajax({
        url: '/Kategori/Sil/' + gelenID,
        type: "POST",
        dataType: 'json',
        success: function (response) {
            if (response.Success) {
                $.notify(response.Message, "success");
                silTR.fadeOut(300, function () {
                    silTR.remove();
                })
            }
            else {
                $.notify(response.Message, "error");
            }


        }

    })
})


function KategoriDuzenle() {

    Kategori = new Object();
    Kategori.KategoriAdi = $("#kategoriAdi").val();
    Kategori.Url = $("#kategoriUrl").val();
    Kategori.AktifMi = $("#kategoriAktif").is(":checked");
    Kategori.ParentID = $("#ParentID").val();
    Kategori.ID = $("#ID").val();

    $.ajax({
        url: "/Kategori/Duzenle",
        data: Kategori,
        type: "POST",
        dataType: 'json',
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });

            }
            else {
                bootbox.alert(response.Message, function () {
                    // geridöndüğünde bir şey yapılması isteniyorsa buraya yazılır
                });
            }
        }
    })
}
