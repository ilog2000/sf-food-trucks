function buildTable(headers, fields, data) {

  function buildHead(headers) {
    const cells = headers.map(h => `<th>${h}</th>`).join('');
    return `<thead><tr>${cells}</tr></thead>`;
  }

  function buildBody(fields, data) {
    return data.map(row => {
      const cells = fields.map(f => `<td>${row[f]}</td>`).join('');
      return `<tr>${cells}</tr>`;
    }).join('');
  }

  return `<table class="table table-striped">${buildHead(headers)}<tbody>${buildBody(fields, data)}</tbody></table>`;
}

function searchForm() {
  const FORM_URL = '/api/foodtrucks/search';
  const headers = ['Name','Type','Address','Description','Food','Location'];
  const fields = ['name','facilityType','address','locationDescription','foodItems','location'];

  return {
    // initial form data
    formData: {
      latitude: null,
      longitude: null,
      preferredFood: '',
    },
    // form submit function
    submitForm() {
      const pageSize = document.getElementById('nr-of-results').value;

      fetch(`${FORM_URL}?pageSize=${pageSize}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Accept: 'application/json',
        },
        body: JSON.stringify(this.formData),
      })
      .then(response => response.json())
      .then((data) => {
        const result = buildTable(headers, fields, data);
        document.getElementById('search-results').innerHTML = result;
      })
      .catch(() => {
        this.formMessage = 'Something went wrong.';
      });
    }
  };
}
