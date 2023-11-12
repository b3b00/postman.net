log(`pm.response.code::>${pm.response.code}<`);
log(`pm.response.status::>${pm.response.status}<`);
const data = pm.response.json();

log(data);
pm.test('this is a test ', () => {
    pm.expect(pm.response.code).to.equal(200);
    pm.expect(data).to.be.an('object');

    
    for (var p in data) {
        log(p+ " :: "+data[p]);
        pm.expect(p).to.match(/^\\d{4}-\\d{2}-\\d{2}$/, "Date format is not 'YYYY-MM-DD");        
        pm.expect(p.substring(0, 4)).to.eq('2025');
        pm.expect(data[p]).to.be.a('string');
    }
    
});