$(document).ready(function () {
    $('#userTable').DataTable();
});

// Silme Modalına ID Aktarma
function setDeleteUserId(id) {
    document.getElementById('deleteUserId').value = id;
}

// Düzenleme Modalına Kullanıcı Bilgilerini Aktarma
function setEditUserId(id, name, email, role) {
    document.getElementById('editUserId').value = id;
    document.getElementById('editUserFirstName').value = name;
    document.getElementById('editUserEmail').value = email;
    document.getElementById('editUserRole').value = role;
}
