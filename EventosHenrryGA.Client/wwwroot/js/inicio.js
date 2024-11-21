console.log('Función descargarArchivo cargada:', typeof descargarArchivo);

function descargarArchivo(base64Data) {
    console.log('Descargando archivo...');
    var link = document.createElement("a");
    link.href = "data:application/pdf;base64," + base64Data;
    link.download = "archivo.pdf";  // Si no tienes el nombre, puedes darle un nombre predeterminado
    link.click();
}
