log(`pm.response.code::>${pm.response.code}<`);
log(`pm.response.status::>${pm.response.status}<`);
const data = pm.response.json();

log(data);
pm.test('this is a test ',() => {
   pm.expect(pm.response.code).to.equal(200); 
});