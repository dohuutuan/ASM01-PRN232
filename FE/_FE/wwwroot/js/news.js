async function loadNews() {
    const res = await fetch('/api/news?$top=10');
    const data = await res.json();
    const tbody = document.getElementById('newsTableBody');
    tbody.innerHTML = "";
    data.value.forEach(n => {
        tbody.innerHTML += `<tr>
            <td>${n.newsTitle}</td>
            <td>${n.categoryName}</td>
            <td>${n.tagNames.join(", ")}</td>
            <td>${n.status ? "Active" : "Inactive"}</td>
            <td>
                <button onclick="editNews(${n.newsArticleId})" class="btn btn-sm btn-primary">Edit</button>
                <button onclick="deleteNews(${n.newsArticleId})" class="btn btn-sm btn-danger">Delete</button>
            </td>
        </tr>`;
    });
}

function openModal() {
    document.getElementById('newsId').value = "";
    document.getElementById('newsTitle').value = "";
    new bootstrap.Modal(document.getElementById('newsModal')).show();
}
