const form = document.querySelector("#searchConditionForm");
const offsetRowsHidden = document.querySelector("input[name='SearchCondition.OffsetRows']");
const sortItemHidden = document.querySelector("input[name='SearchCondition.SortItem']");
const sortTypeHidden = document.querySelector("input[name='SearchCondition.SortType']");
const searchButton = document.querySelector("#searchButton");
const clearButton = document.querySelector("#clearButton");
const table = document.querySelector("table");
const resultMessage = document.querySelector("#resultMessage");
const tableHeader = document.querySelector("#tableHeader");
const sortButtons = document.querySelectorAll(".btn-sort");
const tableBody = document.querySelector("#tableBody");
const tableLoading = document.querySelector("#tableLoading");
const scrollToTopButton = document.querySelector("#scrollToTopButton");
const token = document.querySelector("[name='__RequestVerificationToken']").value;
let isFetchInProgress = false;
let totalRecordsCount = 0;
let searchParams;
// 検索ボタンクリックイベント
searchButton.addEventListener("click", () => search());
// Enterキー押下イベント
form.addEventListener("keydown", (e) => {
    if (e.keyCode === 13) {
        search();
        e.preventDefault();
    }
});
// クリアボタンクリックイベント
clearButton.addEventListener("click", () => {
    form.reset();
    sortItemHidden.value = "";
    sortTypeHidden.value = "";
    clearResult();
});
// 各列のソートボタンクリックイベント
sortButtons.forEach((button) => {
    button.addEventListener("click", (e) => {
        let sortItem = e.currentTarget.getAttribute("data-sort-item");
        sortTypeHidden.value = sortItemHidden.value == sortItem && sortTypeHidden.value == "asc" ? "desc" : "asc";
        sortItemHidden.value = sortItem;
        search();
    });
});
// トップへ戻るボタンクリックイベント
scrollToTopButton.addEventListener("click", () => window.scroll({ top: 0, behavior: "smooth" }));
// スクロールイベント
window.addEventListener("scroll", () => {
    const { scrollHeight, scrollTop, clientHeight } = document.documentElement;
    // 次の検索結果行をフェッチ
    if (!isFetchInProgress
        && scrollHeight <= scrollTop + clientHeight
        && parseInt(offsetRowsHidden.value, 10) < totalRecordsCount) {
        fetchResultRows();
    }
    // トップへ戻るボタンの表示制御
    if (140 < scrollTop) {
        scrollToTopButton.classList.remove("d-none");
    } else {
        scrollToTopButton.classList.add("d-none");
    }
});
// 検索結果のクリア
function clearResult() {
    searchParams = null;
    offsetRowsHidden.value = "0";
    totalRecordsCount = 0;
    resultMessage.textContent = "";
    tableHeader.classList.add("d-none");
    tableBody.innerHTML = "";
    sortButtons.forEach((button) => {
        if (sortItemHidden.value == button.getAttribute("data-sort-item")) {
            button.innerHTML = sortTypeHidden.value == "asc"
                ? "<i class='bi bi-sort-alpha-down'></i>"
                : "<i class='bi bi-sort-alpha-down-alt'></i>";
        } else {
            button.innerHTML = "<i class='bi bi-arrow-down-up'></i>";
        }
    });
}
// 検索開始
function search() {
    clearResult();
    searchParams = new URLSearchParams(new FormData(form));
    fetchResultRows();
}
// 検索実行と結果行のフェッチ
function fetchResultRows() {
    isFetchInProgress = true;
    tableLoading.classList.remove("d-none");
    searchParams.set("SearchCondition.OffsetRows", offsetRowsHidden.value);
    fetch("?Handler=Search", { method: "POST", body: searchParams })
        .then((response) => {
            if (!response.ok) {
                throw new Error("読み込みに失敗しました");
            }
            totalRecordsCount = parseInt(response.headers.get("X-total-records-count"), 10);
            if (offsetRowsHidden.value == 0 || 0 < totalRecordsCount) {
                resultMessage.textContent = `件数: ${totalRecordsCount}`;
                if (0 < totalRecordsCount) {
                    tableHeader.classList.remove("d-none");
                }
            }
            if (response.headers.has("X-last-seq")) {
                offsetRowsHidden.value = response.headers.get("X-last-seq");
            }
            return response.text();
        })
        .then((text) => {
            tableBody.insertAdjacentHTML("beforeend", text);
        })
        .catch((error) => {
            resultMessage.innerHTML = error;
        })
        .finally(() => {
            tableLoading.classList.add("d-none");
            isFetchInProgress = false;
        });
}
