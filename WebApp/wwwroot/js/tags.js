function initTagSelector(config) {
    let activeIndex = -1;
    let selectedIds = [];

    const tagContainer = document.getElementById(config.containerId);
    const tagsList = tagContainer.querySelector('.tags-list');
    const input = document.getElementById(config.inputId);
    const results = document.getElementById(config.resultsId);
    const searchResults = document.querySelector('.search-results');
    const selectedInputContainer = document.getElementById(config.selectedInputIds);

    if (Array.isArray(config.preselected)) {
        config.preselected.forEach(item => addTag(item));
    }

    input.addEventListener('focus', () => {
        tagContainer.classList.add('focused');
        results.classList.add('focused');
    });

    input.addEventListener('input', () => {
        const term = input.value.trim();
        activeIndex = -1;

        if (!term) {
            results.style.display = 'none';
            results.innerHTML = '';
            return;
        }

        fetch(config.searchUrl(term))
            .then(r => r.json())
            .then(data => renderSearchResults(data))
            .catch(() => {
                results.innerHTML = '';
                results.style.display = 'none';
            });
    });

    input.addEventListener('keydown', e => {
        const items = results.querySelectorAll('.search-item');
        switch (e.key) {
            case 'ArrowDown':
                e.preventDefault();
                if (items.length) {
                    activeIndex = (activeIndex + 1) % items.length;
                    updateActiveItem(items);
                }
                break;
            case 'ArrowUp':
                e.preventDefault();
                if (items.length) {
                    activeIndex = (activeIndex - 1 + items.length) % items.length;
                    updateActiveItem(items);
                }
                break;
            case 'Enter':
                e.preventDefault();
                if (activeIndex >= 0) items[activeIndex].click();
                break;
            case 'Backspace':
                if (!input.value) removeLastTag();
                break;
        }
    });

    function addTag(item) {
        if (selectedIds.includes(item.id)) return;
        selectedIds.push(item.id);

        const tag = document.createElement('div');
        tag.classList.add(config.tagClass || 'tag');
        tag.innerHTML = `
            ${item[config.imageProperty]
                ? `<img src="${item[config.imageProperty]}" class="search-user-avatar" />`
                : ``
            }
            <span>${item[config.displayProperty]}</span>
        `;

        const removeBtn = document.createElement('span');
        removeBtn.classList.add('_btn-close');
        removeBtn.dataset.id = item.id;
        removeBtn.addEventListener('click', e => {
            selectedIds = selectedIds.filter(i => i !== item.id);
            tag.remove();
            updateSelectedIdsInput();
            e.stopPropagation();
        });

        tag.appendChild(removeBtn);
        tagsList.appendChild(tag);

        input.value = '';
        results.innerHTML = '';
        results.style.display = 'none';

        updateSelectedIdsInput();
    }

    function renderSearchResults(data) {
        results.innerHTML = '';
        if (!Array.isArray(data) || !data.length) {
            const d = document.createElement('div');
            d.classList.add('search-item');
            d.textContent = config.emptyMessage || 'No results.';
            results.appendChild(d);
        } else {
            data.forEach(item => {
                if (!selectedIds.includes(item.id)) {
                    const r = document.createElement('div');
                    r.classList.add('search-item');
                    r.dataset.id = item.id;
                    r.innerHTML = `
                        ${item[config.imageProperty]
                            ? `<img src="${item[config.imageProperty]}" class="search-user-avatar" />`
                            : ``
                        }
                        <span>${item[config.displayProperty]}</span>
                    `;
                    r.addEventListener('click', () => addTag(item));
                    results.appendChild(r);
                }
            });
        }
        results.style.display = 'block';
    }

    function removeLastTag() {
        const allTags = tagsList.querySelectorAll(`.${config.tagClass || 'tag'}`);
        if (!allTags.length) return;
        const last = allTags[allTags.length - 1];
        const id = last.querySelector('._btn-close').dataset.id;
        selectedIds = selectedIds.filter(i => i !== id);
        last.remove();
        updateSelectedIdsInput();
    }

    function updateSelectedIdsInput() {
        if (!selectedInputContainer) return;
        selectedInputContainer.innerHTML = '';
        const name = config.selectedInputName || config.selectedInputIds.replace(/Container$/, '');
        selectedIds.forEach(id => {
            const h = document.createElement('input');
            h.type = 'hidden';
            h.name = name;
            h.value = id;
            selectedInputContainer.appendChild(h);
        });
    }

    function updateActiveItem(items) {
        items.forEach(i => i.classList.remove('active'));
        if (items[activeIndex]) {
            items[activeIndex].classList.add('active');
            items[activeIndex].scrollIntoView({ block: 'nearest' });
        }
    }

    document.addEventListener('click', function (e) {
        if (!searchResults.contains(e.target)) {
            results.style.display = 'none';
            tagContainer.classList.remove('focused');
        }
    });

    return { addTag };
}

document.addEventListener('DOMContentLoaded', function () {
    //Add Project Tag Selector
    const addEl = document.getElementById('tagged-members');
    if (addEl) {
        initTagSelector({
            containerId: 'tagged-members',
            inputId: 'member-search',
            resultsId: 'member-search-results',
            selectedInputIds: 'MembersIdsContainer',
            selectedInputName: 'MembersIds',
            searchUrl: term => addEl.dataset.searchUrl + '?term=' + encodeURIComponent(term),
            displayProperty: 'fullName',
            imageProperty: 'imageUri',
            tagClass: 'tag',
            emptyMessage: 'No members with that name found.',
            preselected: []
        });
    }

    //Edit Project Tag Selector 
    const editEl = document.getElementById('tagged-users-edit');
    if (editEl) {
        window.editProjectSelector = initTagSelector({
            containerId: 'tagged-users-edit',
            inputId: 'member-search-edit',
            resultsId: 'member-search-results-edit',
            selectedInputIds: 'Edit_MembersIdsContainer',
            selectedInputName: 'MembersIds',
            searchUrl: term => editEl.dataset.searchUrl + '?term=' + encodeURIComponent(term),
            displayProperty: 'fullName',
            imageProperty: 'imageUri',
            tagClass: 'tag',
            emptyMessage: 'No members with that name found.',
            preselected: []
        });
    }
});