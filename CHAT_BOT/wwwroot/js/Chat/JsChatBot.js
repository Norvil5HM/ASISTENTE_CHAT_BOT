const d = document;
const $msg_menu_ayuda = d.querySelector(".msg_menu_ayuda");
const $contenedor_consultas_respuestas = d.querySelector(".contenedor_consultas_respuestas");

let arrayMenuAsistente = [];

d.addEventListener("DOMContentLoaded", e => {
    $contenedor_consultas_respuestas.innerHTML = "";

    Uf_cargar_lista_menu($msg_menu_ayuda);
});

// --
function Uf_cargar_lista_menu($msg_menu_ayuda) {

    $msg_menu_ayuda.innerHTML = "";

    fetch("/ChatBot/Lista_menu_asistente", {
        method: "GET",
        headers: {
            "Content-Type": "application/json; charset=utf-8",
        },
    })
        .then((res) => (res.ok ? res.json() : Promise.reject(res)))
        .then((json) => {
            if (json.mensaje == "" && (json.objeto != null || json.listaObjeto != null)) {

                arrayMenuAsistente = json.listaObjeto || json.objeto;

                json.listaObjeto.forEach(menu => {
                    $msg_menu_ayuda.innerHTML += ` <span>${menu.id_menu_asistente} : ${menu.desc_menu_asistente}</span><br>`;
                });
            } else if (json.mensaje != "") {
                $msg_menu_ayuda.innerHTML = json.mensaje;
            }
        })
        .catch((err) => {
            window.alert(err);
            console.log(err);
        })
        .finally(() => { });
}

// --
function Uf_request_response_usser_chatbot() {
    let request_client = d.querySelector(".input_consulta").value;
    let menu_encontrado = arrayMenuAsistente.find(menu => menu.id_menu_asistente == request_client || menu.desc_menu_asistente == request_client);


    if (request_client === "0") {
        $contenedor_consultas_respuestas.innerHTML = "";
        return;

    } else if (menu_encontrado == null && request_client != "") {
        $contenedor_consultas_respuestas.innerHTML += `
        <div class="alert mt-5 msg_consulta">
                        <strong>Tú:</strong><br>
                        <span>
                           ${request_client}
                       </span>
        </div>

    `;
        $contenedor_consultas_respuestas.innerHTML += `
        <div class="alert  mt-5 msg_respuesta">
                    <strong>Asistente:</strong><br>
                    <span class="span_response">
                        
                    </span>
                </div>
        `;

        ScrollAutomatico();
        let arraySpanResponses = Array.from(d.querySelectorAll(".span_response"));
        Uf_escribir_response(arraySpanResponses[arraySpanResponses.length - 1], "Solicitud incorrecta o inválida", 20);
        return;

    } else if (menu_encontrado != null) {
        $contenedor_consultas_respuestas.innerHTML += `
        <div class="alert mt-5 msg_consulta">
                        <strong>Tú:</strong><br>
                        <span>
                            ${menu_encontrado.id_menu_asistente}:${menu_encontrado.desc_menu_asistente}
                       </span>
        </div>
    `;
        ScrollAutomatico();
    }


    //// enviar request y recepcionar response
    fetch("/ChatBot/Lista_paso_menu", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            Id_menu_asistente: menu_encontrado.id_menu_asistente
        })
    })
        .then((res) => (res.ok ? res.json() : Promise.reject(res)))
        .then((json) => {
            if (json.mensaje == "" && (json.objeto != null || json.listaObjeto != null)) {
                let ArrayPasos = json.listaObjeto || json.objeto;
                let pasos_response = "";

                $contenedor_consultas_respuestas.innerHTML += `
                   <div class="alert  mt-5 msg_respuesta">
                    <strong>Asistente:</strong><br>
                    <span class="span_response">
                    </span>
                 </div>
                 `;

                let arraySpanResponses = Array.from(d.querySelectorAll(".span_response"));
                let contador = 0;

                ArrayPasos.forEach(paso => {
                    contador++;
                    pasos_response += `${contador}.${paso.desc_paso_menu}<br>`;
                });

                Uf_escribir_response(arraySpanResponses[arraySpanResponses.length - 1], pasos_response, 10);


            } else if (json.mensaje != "") {
                $contenedor_consultas_respuestas.innerHTML += `
                   <div class="alert  mt-5 msg_respuesta">
                    <strong>Asistente:</strong><br>
                    <span class="span_response">
                        
                    </span>
                 </div>
                 `;

                ScrollAutomatico();
                let arraySpanResponses = Array.from(d.querySelectorAll(".span_response"));
                Uf_escribir_response(arraySpanResponses[arraySpanResponses.length - 1], json.mensaje, 20);
            }
        })
        .catch((err) => {
            window.alert(err);
            console.log(err);
        })
        .finally(() => { });
}

// 
function ScrollAutomatico() {
    var chatContainer = d.querySelector(".contenedor_consultas_respuestas");
    chatContainer.scrollTop = chatContainer.scrollHeight;
}

//
function Uf_escribir_response(span_response, response, delay) {

    let index = 0;
    let _span_response = span_response;
    let _response = response;
    let _delay = delay;

    function escribir() {
        if (index < _response.length) {
            let currentChar = _response.charAt(index);
            if (currentChar === '<') {
                let tagEndIndex = _response.indexOf('>', index);
                if (tagEndIndex !== -1) {
                    let htmlTag = _response.slice(index, tagEndIndex + 1);
                    _span_response.innerHTML += htmlTag;
                    index = tagEndIndex + 1;
                } else {
                    index++;
                }
            } else {
                _span_response.innerHTML += currentChar;
                index++;
            }
            ScrollAutomatico();
            setTimeout(escribir, _delay);
        }
    }

    escribir();
}