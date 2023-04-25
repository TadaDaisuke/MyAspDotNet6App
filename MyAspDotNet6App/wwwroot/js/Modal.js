const detailModal = new bootstrap.Modal(document.querySelector("#detailModal"));
const modalBodyLoading = document.querySelector("#modalBodyLoading");
const modalBodyContent = document.querySelector("#modalBodyContent");
const saveButton = document.querySelector("#saveButton");
const closeModalButton = document.querySelector("#closeModalButton");
// 検索結果各行のクリックイベント
table.addEventListener("click", (e) => {
    let detailKey = e.target.closest("tr").getAttribute("data-detail-key");
    if (detailKey) {
        modalBodyContent.classList.add("d-none");
        modalBodyLoading.classList.remove("d-none");
        // メンバー詳細の読み込み
        let formData = new FormData();
        formData.append("detailKey", detailKey);
        formData.append("__RequestVerificationToken", token);
        fetch("?Handler=GetDetail", { method: "POST", body: formData })
            .then((response) => {
                if (!response.ok) {
                    throw new Error("読み込みに失敗しました");
                }
                return response.text();
            })
            .then((text) => {
                modalBodyContent.innerHTML = text;
                let newForm = document.querySelector("#detailForm");
                $(newForm).removeData("validator");
                $(newForm).removeData("unobtrusiveValidation");
                $.validator.unobtrusive.parse($(newForm));
                enableSaveButton(newForm);
                newForm.querySelectorAll("input").forEach((inputElem) => inputElem.addEventListener("input", () => enableSaveButton(newForm)));
            })
            .catch((error) => {
                modalBodyContent.innerHTML = `<div>${error}</div>`;
            })
            .finally(() => {
                modalBodyLoading.classList.add("d-none");
                modalBodyContent.classList.remove("d-none");
            });
        // モーダルの表示
        detailModal.show();
    }
});
// 保存ボタンの活性制御
function enableSaveButton(newFormElem) {
    let canSave = false;
    if ($(newFormElem).valid()) {
        newFormElem.querySelectorAll("input[data-original-value]").forEach((inputElem) => {
            if (inputElem.value != inputElem.getAttribute("data-original-value")) {
                canSave = true;
            }
        });
    }
    if (canSave) {
        saveButton.removeAttribute("disabled");
    } else {
        saveButton.setAttribute("disabled", "disabled");
    }
}
// モーダルの保存ボタンクリックイベント
saveButton.addEventListener("click", () => {
    detailModal.hide();
    let formData = new FormData(document.querySelector("#detailForm"));
    formData.append("__RequestVerificationToken", token);
    fetch("?Handler=SaveDetail", { method: "POST", body: formData })
        .then((response) => {
            if (!response.ok) {
                throw new Error("保存に失敗しました");
            }
            return response.text();
        })
        .then((text) => {
            modalBodyContent.classList.add("d-none");
            modalBodyLoading.classList.remove("d-none");
            alert(text);
            search();
        })
        .catch((error) => {
            alert(error);
            detailModal.show();
        });
});
// モーダルの閉じるボタンクリックイベント
closeModalButton.addEventListener("click", () => detailModal.hide());
