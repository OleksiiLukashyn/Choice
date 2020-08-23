async function send() {
    const url = '/Home/EditAjax';

    var disciplines = [];
    var elements = document.getElementsByClassName("discipline");
    for (let i = 0; i < elements.length; i++) {
        disciplines.push(
            {
                "id": parseInt(elements[i].children[3].value),
                "name": elements[i].children[2].innerText,
                "isStudied": elements[i].children[0].checked,
            }
        );
    }


    var studentData = document.getElementById("student");
    var student = {
        id: parseInt(studentData.children[0].value),
        name: studentData.children[1].value,
        group: studentData.children[2].value
    }


    let data = JSON.stringify({ student: student, disciplines: disciplines });

    let xsrf_token = document.getElementsByName("__RequestVerificationToken")[0].value;

    try {
        const response = await fetch(url, {
            method: 'POST',
            body: data,
            credentials: 'include',
            headers: {
                "XSRF-TOKEN": xsrf_token,
                "Content-Type": "application/json"
            }
        });
        if (response.status === 200) {
            const json = await response.json();
            add_message(json.name);
        }
        else {
            const text = await response.text();
            alert(text);
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

function add_message(name) {
    const html = `
<div class="toast m-3 border border-success bg-light" role="alert" aria-live="assertive" aria-atomic="true" data-autohide="false" style="position: absolute; top: 0; right: 0;">
  <div class="toast-header bg-light">
    <strong class="mr-auto">Update notification</strong>
    <small>now</small>
    <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="toast-body text-success">
    Disciplines for ${name} have been successfully updated
  </div>
</div>`;

    const item = document.getElementById("top");
    item.innerHTML = html;
    $('.toast').toast('show');
}